namespace ExamplesCore.Structures.StructureDtos;

public class PostRequestResponse<T> : IControllerResponse
{
    public PostRequestResponse(T data)
    {
        Data = data;
    }

    public object? Data { get;  }
}