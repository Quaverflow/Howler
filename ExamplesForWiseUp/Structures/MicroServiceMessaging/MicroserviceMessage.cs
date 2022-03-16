namespace ExamplesForWiseUp.Models;

public record MicroserviceMessage(string Path, HttpMethod Method, object? Payload = null);