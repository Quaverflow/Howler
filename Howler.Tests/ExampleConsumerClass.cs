using System.Threading.Tasks;

namespace Howler.Tests;

public class ExampleConsumerClass
{
    private readonly IHowler _howler;

    public ExampleConsumerClass(IHowler howler)
    {
        _howler = howler;
    }

    public int SimpleMethod(string s) => _howler.Invoke(() => ExampleStaticClass.ReturnLength(s));
    public async Task<int> ComplexMethod(string s)
    {
        var y = _howler.Invoke(() => ExampleStaticClass2.ReturnLength(s));
        var x = await _howler.InvokeAsync(() => ExampleStaticClass.ReturnLengthAsync("hello"));
        var z = _howler.Invoke(() => ExampleStaticClass.ReturnLength(s));
        return z + x * y;
    }
}