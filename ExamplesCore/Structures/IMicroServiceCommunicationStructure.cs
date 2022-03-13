using ExamplesCore.Structures.StructureDtos;

namespace ExamplesCore.Structures;

public interface IMicroServiceCommunicationStructure
{
    Task<HttpResponseMessage> SendToMicroService(MicroServiceCommunicationStructureData data);
}