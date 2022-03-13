using ExamplesCore.Models;
using ExamplesCore.Structures.StructureDtos;

namespace ExamplesCore.Structures;

public interface IMicroServiceCommunicationStructure
{

    Task<MicroServiceResult?> SendToMicroService(MicroServiceCommunicationStructureData data);
}