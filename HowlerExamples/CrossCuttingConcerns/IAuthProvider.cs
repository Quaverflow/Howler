namespace HowlerExamples.CrossCuttingConcerns;

public interface IAuthProvider
{
    bool HasAccess(bool yesNo);
}