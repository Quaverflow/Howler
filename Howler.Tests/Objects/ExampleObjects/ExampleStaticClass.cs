using System;
using System.Threading.Tasks;

namespace Howler.Tests.Objects.ExampleObjects;

public static class ExampleStaticClass
{
    public static bool Check { get; set; }
    public static int ReturnLength(string s) => s.Length;
    public static int ReturnLength(string s, int i) => i + s.Length;
    public static async Task<int> ReturnLengthAsync(string s) => await Task.FromResult(s.Length);
    public static async Task ReturnAsync()
    {
        Check = true;
        await Task.CompletedTask;
    }

    public static void VoidLength() => throw new Exception("I was called!");
}