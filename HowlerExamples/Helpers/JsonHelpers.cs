using Newtonsoft.Json;

namespace HowlerExamples.Helpers;

public static class JsonHelpers
{
    public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);
}