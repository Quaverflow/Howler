using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Howler;

public static class Guards
{
    public static T ThrowIfNull<T>([NotNull] this T? argument, [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (argument == null)
            throw new ArgumentNullException(paramName);
        return argument;
    }
}