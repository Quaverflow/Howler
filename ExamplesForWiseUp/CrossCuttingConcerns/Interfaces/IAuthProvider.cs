namespace ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;

public interface IAuthProvider
{
    Task HasAccess(Guid id);
}