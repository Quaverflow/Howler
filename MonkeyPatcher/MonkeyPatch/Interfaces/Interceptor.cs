using Castle.DynamicProxy;

namespace MonkeyPatcher.MonkeyPatch.Interfaces;

internal class Interceptor : IInterceptor
{
    internal readonly Delegate Original;
    private readonly Delegate? _callback;
    private readonly bool _isVoid;
    private readonly bool _isProperty;
    private object? _propertyValueSetAfterSetup;
    private bool _getInvocation;
    public Interceptor(Delegate original, Delegate? callback, bool isVoid, bool isProperty, bool getInvocation)
    {
        Original = original;
        _callback = callback;
        _isVoid = isVoid;
        _isProperty = isProperty;
        _getInvocation = getInvocation;
    }



    public void Intercept(IInvocation invocation)
    {

        if (!_isVoid)
        {
            try
            {
                if (_isProperty)
                {
                    ProcessProperty(invocation);
                }
                else
                {
                    invocation.ReturnValue = _getInvocation ? _callback?.DynamicInvoke(invocation): _callback?.DynamicInvoke();
                }
            }
            catch (Exception e)
            {
                throw e.InnerException!;
            }
        }
        else
        {
            invocation.ReturnValue = _getInvocation ? _callback?.DynamicInvoke(invocation) : _callback?.DynamicInvoke();

        }
    }

    private void ProcessProperty(IInvocation invocation)
    {
        if (invocation.Arguments.Any())
        {
            _callback?.DynamicInvoke();
            _propertyValueSetAfterSetup = invocation.Arguments[0];
            invocation.ReturnValue = _propertyValueSetAfterSetup;
        }
        else
        {
            if (_propertyValueSetAfterSetup != null)
            {
                _callback?.DynamicInvoke();
                invocation.ReturnValue = _propertyValueSetAfterSetup;

            }
            else
            {
                invocation.ReturnValue = _callback?.DynamicInvoke();
            }
        }
    }
}