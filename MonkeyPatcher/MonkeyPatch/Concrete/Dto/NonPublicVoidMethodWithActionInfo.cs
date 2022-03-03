using Utilities;

namespace MonkeyPatcher.MonkeyPatch.Concrete.Dto;

public sealed record NonPublicVoidMethodWithActionInfo(string MethodName, AccessType Type, Action actual, params Type?[]? MethodParameters);