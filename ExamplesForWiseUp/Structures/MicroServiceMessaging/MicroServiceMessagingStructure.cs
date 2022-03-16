using System.Text;
using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Helpers;
using Howler;

namespace ExamplesForWiseUp.Structures.MicroServiceMessaging;

public class MicroServiceMessagingStructure : IHowlerStructure
{
    private readonly HttpClient _client;
    private readonly IFakeLogger _logger;

    public MicroServiceMessagingStructure(IHttpClientFactory httpClientFactory, IFakeLogger logger)
    {
        _logger = logger;
        _client = httpClientFactory.CreateClient();
    }

    public async Task MessageMicroService(MicroserviceMessage message)
    {
        try
        {

            _logger.Log("Messaging the Micro Service");

            var request = new HttpRequestMessage(message.Method, new Uri(message.Path))
            {
                Content = new StringContent(message.Payload?.ToJson() ?? "", Encoding.UTF8, "application/json")
            };
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            _logger.Log("Micro service responded correctly");
        }
        catch (Exception e)
        {
            _logger.Log($"Micro service failed to response with exception {e.Message}");
        }
    }

    public void InvokeRegistrations()
    {
        HowlerRegistry.AddStructure(StructureIds.NotifyMicroService, MessageMicroService);
    }
}