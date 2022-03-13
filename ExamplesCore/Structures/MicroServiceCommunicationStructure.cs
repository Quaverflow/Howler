using System.Text;
using ExamplesCore.CrossCuttingConcerns;
using ExamplesCore.Models;
using ExamplesCore.Structures.StructureDtos;
using Newtonsoft.Json;
using Utilities;

namespace ExamplesCore.Structures;

public class MicroServiceCommunicationStructure : IMicroServiceCommunicationStructure
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IFakeLogger _logger;

    public MicroServiceCommunicationStructure(IHttpClientFactory httpClientFactory, IFakeLogger logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<MicroServiceResult?> SendToMicroService(MicroServiceCommunicationStructureData data)
    {
        try
        {
            _logger.Log("Begin notifying MicroService");

            data.ThrowIfNull();


            var message = new HttpRequestMessage(data.Method, data.Uri);
            if (data.Payload != null)
            {
                message.Content = new StringContent(data.Payload, Encoding.UTF8, "application/json");
            }

            var result = await _httpClientFactory.CreateClient().SendAsync(message);

            result.EnsureSuccessStatusCode();
            _logger.Log("MicroService notified");

            return JsonConvert.DeserializeObject<MicroServiceResult>(await result.Content.ReadAsStringAsync());

        }
        catch (Exception e)
        {
            _logger.Log($"MicroService notification failed with error {e.Message}");
            throw;
        }
    }
}