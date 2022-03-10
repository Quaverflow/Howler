using Utilities;

namespace HowlerExamples.Services;

public class AuthProvider : IAuthProvider
{
    public bool HasAccess(bool yesNo)
    {
        yesNo.ThrowIfAssumptionFailed("You're not authorized to see this.");
        return yesNo;
    }
}