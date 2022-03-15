//using System;
//using System.Threading.Tasks;

//namespace Howler.Tests.Objects.ExampleObjects;

//public class ExampleConsumerClass
//{
//    private readonly IHowler _howler;
//    private readonly ExampleInstanceClass _instanceClass;

//    public ExampleConsumerClass(IHowler howler)
//    {
//        _howler = howler;
//        _instanceClass = new ExampleInstanceClass();
//    } 

//    public int SimpleMethod(string s) => _howler.Invoke(() => ExampleStaticClass.ReturnLength(s), TestStructuresIds.Structure1);

//    public async Task<int> ComplexMethod(string s)
//    {
//        var y = _howler.Invoke(() => ExampleStaticClass2.ReturnLength(s), TestStructuresIds.Structure1);
//        var x = await _howler.Invoke(() => ExampleStaticClass.ReturnLengthAsync("hello"), TestStructuresIds.Structure2);
//        var z = _howler.Invoke(() => ExampleStaticClass.ReturnLength(s), TestStructuresIds.Structure3);
//        return z + x * y;
//    }

//    public int CallInstance() => _howler.Invoke(() => _instanceClass.Three(), TestStructuresIds.Structure1);
//}

//public static class TestStructuresIds
//{
//    public static Guid Structure1 = Guid.NewGuid();
//    public static Guid Structure2 = Guid.NewGuid();
//    public static Guid Structure3 = Guid.NewGuid();
//    public static Guid Structure4 = Guid.NewGuid();
//    public static Guid Structure5 = Guid.NewGuid();
//}