namespace HowlerExamples.Services;

public interface IAuthProvider
{
    bool HasAccess(bool yesNo);
}