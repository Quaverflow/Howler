namespace ExamplesCore.Structures.StructureDtos;

public class PostRequestResponse<T> : IControllerResponse
{
    public PostRequestResponse(T data)
    {
        Data = data;
    }

    public object? Data { get;  }
}

public interface IControllerResponse
{
    public object? Data { get; }
}