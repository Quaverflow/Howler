using Newtonsoft.Json;

namespace ExamplesForWiseUp.Helpers;

public static class JsonHelpers
{
    public static string? ToJson(this object obj) => JsonConvert.SerializeObject(obj);
}