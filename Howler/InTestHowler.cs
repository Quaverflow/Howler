using System.Linq.Expressions;
using Utilities.ExtensionMethods;

namespace Howler;

public class InTestHowler : IHowler
{
    public TResult Invoke<TResult>(Func<TResult> original, Guid? id = null) => original.Invoke();
    public void InvokeVoid(Action original, Guid? id = null) => original.Invoke();
}