using System.Text.Json.Serialization;

namespace MonkeyPatcher.MonkeyPatch.Concrete;

public class MethodStructure
{
    public MethodStructure(string key, int depth, int stackPosition)
    {
        Key = key;
        Depth = depth;
        StackPosition = stackPosition;
    }
    public string Signature { get; set; }
    public string? Owner { get; set; }
    public int Depth { get; set; }
    public int StackPosition { get; set; }
    public string? ReturnType { get; set; }
    [JsonIgnore]
    public Delegate Action { get; set; }
    [JsonIgnore]
    public bool IsDetoured { get; set; }
    [JsonIgnore]
    public string Key { get; set; }
    public List<MethodStructure> SubNodes { get; set; } = new();
    public List<MethodStructure> SuperNodes { get; set; } = new();
    public List<int> Indexes { get; set; } = new();
}