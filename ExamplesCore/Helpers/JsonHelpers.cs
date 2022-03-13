using Newtonsoft.Json;

namespace ExamplesCore.Helpers;

public static class JsonHelpers
{
    public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);
}