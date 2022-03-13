namespace ExamplesCore.CrossCuttingConcerns;

public interface IAuthProvider
{
    bool HasAccess(bool yesNo);
}