using System.Threading.Tasks;

namespace Howler.Tests.Objects;

public class ExampleConsumerClass
{
    private readonly IHowler _howler;
    private readonly ExampleInstanceClass _instanceClass;

    public ExampleConsumerClass(IHowler howler)
    {
        _howler = howler;
        _instanceClass = new ExampleInstanceClass();
    } 

    public int SimpleMethod(string s) => _howler.Invoke(() => ExampleStaticClass.ReturnLength(s));

    public async Task<int> ComplexMethod(string s)
    {
        var y = _howler.Invoke(() => ExampleStaticClass2.ReturnLength(s));
        var x = await _howler.Invoke(() => ExampleStaticClass.ReturnLengthAsync("hello"));
        var z = _howler.Invoke(() => ExampleStaticClass.ReturnLength(s));
        return z + x * y;
    }

    public int CallInstance() => _howler.Invoke(() => _instanceClass.Three());
}