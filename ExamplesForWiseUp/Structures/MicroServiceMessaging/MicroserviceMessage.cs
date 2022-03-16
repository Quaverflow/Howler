namespace ExamplesForWiseUp.Structures.MicroServiceMessaging;

public record MicroserviceMessage(string Path, HttpMethod Method, object? Payload = null);