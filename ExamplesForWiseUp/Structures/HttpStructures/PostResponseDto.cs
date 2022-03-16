namespace ExamplesForWiseUp.Structures.HttpStructures;

public class PostResponseDto<T> : IHttpStructureDto
{
    public PostResponseDto(T data)
    {
        Data = data;
    }

    public T Data { get; set; }
}

public class GetResponseDto<T> : IHttpStructureDto
{
    public GetResponseDto(T data)
    {
        Data = data;
    }

    public T Data { get; set; }
}