using Castle.DynamicProxy;
using Utilities;

namespace MonkeyPatcher.MonkeyPatch.Interfaces;

public class Proxy<T> where T : class
{
    public T Object => GenerateProxy();
    internal readonly InterfaceSet<Interceptor> Interceptors = new();

    private static readonly ProxyGenerator Generator = new();
    private static readonly ProxyGenerationOptions Options = new() { Selector = new MethodSelector() };
    private T GenerateProxy() =>
        typeof(T).IsInterface 
            ? (T) Generator.CreateInterfaceProxyWithoutTarget(typeof(T), Type.EmptyTypes, Options, Interceptors.ToArray())
            : (T)Generator.CreateClassProxy(typeof(T), Type.EmptyTypes, Options, Interceptors.ToArray());
}


