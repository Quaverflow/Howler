namespace HowlerExamples.Services;

public class NormalService : INormalService
{
    private readonly IFakeLogger _logger;

    public NormalService(IFakeLogger logger)
    {
        _logger = logger;
    }
    public string GetData()
    {
        _logger.Log("The service was called");
        try
        {
            var result = "Hello!";

            _logger.Log("The service call succeeded");
            return result;
        }
        catch (Exception e)
        {
            _logger.Log($"The service failed with exception {e.Message}");
            throw;
        }
    }
}