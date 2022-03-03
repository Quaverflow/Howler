using Utilities;

namespace MonkeyPatcher.MonkeyPatch.Concrete.Dto;

public sealed record NonPublicVoidMethodInfo(string MethodName, AccessType Type, params Type?[]? MethodParameters);