namespace HowlerExamples.Database;

public class Person : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}