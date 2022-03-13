using ExamplesCore.Helpers;

namespace ExamplesCore.Structures.StructureDtos;

public class MicroServiceCommunicationStructureData
{
    public MicroServiceCommunicationStructureData(object? payload, HttpMethod method, Uri uri)
    {
        Payload = payload?.ToJson();
        Method = method;
        Uri = uri;
    }

    public string? Payload { get;  }
    public HttpMethod Method { get;  }
    public Uri Uri { get; }
}