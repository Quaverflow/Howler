using System.Diagnostics;
using System.Linq;
using System.Threading;
using Xunit;

namespace Howler.Tests;

public class PerformanceTest
{
    [Fact]
    public void Test()
    {
        Thread.Sleep(100);
        var stopwatch = new Stopwatch();
        var howler = new Howler();

        var iterations = 100;

        stopwatch.Restart();

        for (var i = 0; i < iterations; i++)
        {
          var p =  Hello();
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
        var p = 
            x.Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a').Append('a')
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
             return "hello" + p;
    }
}