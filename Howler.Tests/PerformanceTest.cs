using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Xunit;

namespace Howler.Tests;

public class PerformanceTest
{
    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    [InlineData(10000)]
    public void Test(int iterations)
    {
        Thread.Sleep(100);
        var stopwatch = new Stopwatch();
        var howler = new Howler(new ServiceContainer());


        stopwatch.Restart();

        for (var i = 0; i < iterations; i++)
        {
            var p = Hello();
        }
        var normal = stopwatch.ElapsedMilliseconds;

        stopwatch.Restart();
        for (var i = 0; i < iterations; i++)
        {
            var p = howler.Invoke(() => Hello());
        }

        var howl = stopwatch.ElapsedMilliseconds;

        Assert.True(true, $"normal{normal}, howler{howl}");
    }


    public string Hello()
    {
        var x = "hopera";
        for (int i = 0; i < 10; i++)
        {


            x+= x.Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
            .Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a');
        }
        return x;
    }
}