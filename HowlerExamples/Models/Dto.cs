using Howler;

namespace HowlerExamples.Models;

public class Dto : IHowlerData
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}

public class DtoGeneric
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}