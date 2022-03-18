using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Helpers;
using Howler;

namespace ExamplesForWiseUp.Whispers;

public class TryCatchWhisper : IHowlerWhisper
{
    private readonly IFakeLogger _logger;

    public TryCatchWhisper(IFakeLogger logger)
    {
        _logger = logger;
    }


    public TResult Try<TResult>(Func<TResult> method)
    {
        _logger.Log("hi");
        try
        {
            return method.Invoke();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<T> ReturnTask<T>(Func<Task<T>> method)
    {
        _logger.Log("bye");
        return await method.Invoke();
    }
    public void Void(Func<string> method)
    {
        _logger.Log(method.Invoke());
    }
}