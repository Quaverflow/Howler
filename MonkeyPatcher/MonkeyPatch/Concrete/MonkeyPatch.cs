using MonkeyPatcher.MonkeyPatch.Concrete.Dto;
using MonoMod.RuntimeDetour;
using System.Linq.Expressions;
using System.Reflection;
using Utilities;
using Utilities.ExtensionMethods;

namespace MonkeyPatcher.MonkeyPatch.Concrete;

public class MonkeyPatch : IDisposable
{
    private static readonly List<IDetour?> Detours = new();
    private static readonly List<MethodStructure> SystemUnderTest = new();
    private static Queue<(int, MethodStructure)> _systemUnderTestCallSpecific = new();
    private readonly Delegate _disposed;
    internal MonkeyPatch(Delegate disposed, MethodInfo caller, int maxScanningDepth)
    {
        _disposed = disposed;
        SystemUnderTest.AddRange(caller.BuildMap(maxScanningDepth));
    }

    private static MethodStructure? GetStructure(MethodInfo original) => SystemUnderTest.FirstOrDefault(x => x.Key == original.GetKey());

    internal void Patch<TReturn>(MethodInfo original, Delegate? actual)
    {
        ProcessStructure(original, actual);

        var returnType = typeof(TReturn);
        returnType.ThrowIfNull();

        if (returnType.BaseType == typeof(Task))
        {
            returnType = typeof(TReturn).GetGenericArguments().FirstOrDefault();
            var func = WrapRefType;
            Detours.Add(new NativeDetour(original, func.Method));
        }
        else if (returnType is { IsClass: true })
        {
            var func = WrapRefType;
            Detours.Add(new NativeDetour(original, func.Method));
        }
        else
        {
            var func = Wrap<TReturn>;
            Detours.Add(new NativeDetour(original, func.Method));
        }
    }

    internal void PatchVoid(MethodInfo original, Delegate? actual)
    {
        ProcessStructure(original, actual);

        var func = WrapRefType;
        Detours.Add(new NativeDetour(original, func.Method));
    }

    private static void ProcessStructure(MethodInfo original, Delegate? actual)
    {
        actual.ThrowIfNull();

        var structure = GetStructure(original);
        if (structure == null)
        {
            throw new NullReferenceException(@"
The structure was null. 
This error is usually caused by the patcher not being disposed.
Make sure you prepend all of your MonkeyPatch instances with 'using'");
        }
        structure.ThrowIfNull();

        structure.IsDetoured = true;
        structure.Action = actual;
    }

    private static TReturn? Wrap<TReturn>(TReturn? returns)
    {
        var patch = GetStructure();
        try
        {
            var result = patch.Action.DynamicInvoke();
            if (result == null)
            {
                return default;
            }
            return (TReturn)result;
        }
        catch (Exception e)
        {
            _systemUnderTestCallSpecific.Clear();
            throw e.InnerException ?? e;
        }
    }

    private static object? WrapRefType()
    {
        var patch = GetStructure();
        try
        {
            var result = patch.Action.DynamicInvoke();
            return result;
        }
        catch (Exception e)
        {
            _systemUnderTestCallSpecific.Clear();
            throw e.InnerException ?? e;
        }
    }

    private static MethodStructure GetStructure()
    {
        if (!_systemUnderTestCallSpecific.Any())
        {
            _systemUnderTestCallSpecific = new Queue<(int, MethodStructure)>();
            var distinct = SystemUnderTest.Where(x => x.IsDetoured).ToList();
            foreach (var structure in distinct)
            {
                foreach (var index in structure.Indexes)
                {
                    _systemUnderTestCallSpecific.Enqueue((index, structure));
                }
            }
        }

        return _systemUnderTestCallSpecific.Dequeue().Item2;
    }

    /// <summary>
    /// For instance methods that should return null or a value, with an optional callback function.
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="expression"></param>
    /// <param name="actual"></param>
       public void Override<TClass, TResult>(Expression<Func<TClass, TResult>> expression, Func<TResult>? actual) where TClass : class
          => Patch<TResult>(GenerateMethodInfo(expression.Body), actual);

    /// <summary>
    /// For static methods that should return null or a value, with an optional callback function.
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="expression"></param>
    /// <param name="actual"></param>
    public void Override<TResult>(Expression<Func<TResult>> expression, Func<TResult>? actual) 
          => Patch<TResult>(GenerateMethodInfo(expression.Body), actual);

    /// <summary>
    /// For instance void methods, with an optional callback.
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="expression"></param>
    /// <param name="actual"></param>
    public void OverrideVoid<TClass>(Expression<Action<TClass>> expression, Action? actual = null) where TClass : class 
        => PatchVoid(GenerateMethodInfo(expression.Body), actual ?? EmptyMethod);


    /// <summary>
    /// For static void methods, with an optional callback.
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="expression"></param>
    /// <param name="actual"></param>
    public void OverrideVoid(Expression<Action> expression, Action? actual = null)
        => PatchVoid(GenerateMethodInfo(expression.Body), actual ?? EmptyMethod);

    private static MethodInfo GenerateMethodInfo(Expression expression) => ((MethodCallExpression)expression).Method;

    /// <summary>
    /// For accessing non public methods with a return type
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="methodInfo"></param>
    public void OverrideNonPublicMethod<TClass, TResult>(NonPublicMethodInfo<TResult> methodInfo) where TClass : class
    {
        var (methodName, accessType, actual, methodParameters) = methodInfo;

        //ReflectionHelpers.FindMethod can handle null methodParameters
        var originalMethod = ReflectionHelpers.FindMethod<TClass>(methodName, accessType.GetBindingFlags(), methodParameters!);
        Patch<TResult>(originalMethod, actual);
    }

    /// <summary>
    /// For accessing non public void methods
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="methodInfo"></param>
    public void OverrideNonPublicVoidMethod<TClass>(NonPublicVoidMethodInfo methodInfo) where TClass : class
    {
        var (methodName, accessType, methodParameters) = methodInfo;

        //ReflectionHelpers.FindMethod can handle null methodParameters
        var originalMethod = ReflectionHelpers.FindMethod<TClass>(methodName, accessType.GetBindingFlags(), methodParameters!);
        PatchVoid(originalMethod, () => 0);
    }

    /// <summary>
    /// For accessing non public void methods
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <param name="methodInfo"></param>
    public void OverrideNonPublicVoidMethodInvokeAction<TClass>(NonPublicVoidMethodWithActionInfo methodInfo) where TClass : class
    {
        var (methodName, accessType, actual, methodParameters) = methodInfo;
        
        //ReflectionHelpers.FindMethod can handle null methodParameters
        var originalMethod = ReflectionHelpers.FindMethod<TClass>(methodName, accessType.GetBindingFlags(), methodParameters!);
        PatchVoid(originalMethod, actual);
    }

    private static void EmptyMethod() { }

    public void Dispose()
    {
        SystemUnderTest.Clear();
        _systemUnderTestCallSpecific.Clear();
        foreach (var x in Detours)
        {
            x?.Dispose();
        }
        Detours.Clear();
        _disposed.DynamicInvoke(true);
        GC.SuppressFinalize(this);
    }
}