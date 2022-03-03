using Utilities;

namespace MonkeyPatcher.MonkeyPatch.Concrete.Dto;

public sealed record NonPublicMethodInfo<TResult>(string MethodName, AccessType Type, Func<TResult> actual, params Type?[]? MethodParameters);