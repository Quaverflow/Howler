using Castle.DynamicProxy;
using System.Reflection;
using Utilities;

namespace MonkeyPatcher.MonkeyPatch.Interfaces;

internal class MethodSelector : IInterceptorSelector
{
    public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
    {
        if (interceptors.Length == 1)
        {
            return interceptors.ToArray();
        }

        var monkeyInterceptors = interceptors as IEnumerable<Interceptor>;
        monkeyInterceptors.ThrowIfNull();

        var matching = monkeyInterceptors.Where(x => x.Original.Method.Equals(method)).ToArray();

        return matching.Length switch
        {
            1 => matching,
            0 => throw new NotImplementedException("You must setup all methods that are being called by your SUT."),
            _ => throw new InvalidOperationException("More than one matching setup where found. Possible bug.")
        };
    }
}