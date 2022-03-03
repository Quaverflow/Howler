using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using MonkeyPatcher.MonkeyPatch.Interfaces;
using MonkeyPatcher.MonkeyPatch.Shared;
using Xunit;

namespace Howler.Tests
{
    public class HowlerBegin
    {
        [Fact]
        public void TestReturns()
        {
            var howler = new Howler();
            var x = howler.Invoke(() => ExampleStaticClass.ReturnLength("hey"));
            Assert.Equal(3, x);
        }

        [Fact]
        public async Task TestReturnsAsync()
        {
            var howler = new Howler();
            var x = await howler.Invoke(() => ExampleStaticClass.ReturnLengthAsync("hey"));
            Assert.Equal(3, x);
        }

        [Fact]
        public void TestVoid()
        {
            var howler = new Howler();
            var ex = Assert.Throws<Exception>(() => howler.Invoke(ExampleStaticClass.VoidLength));
            Assert.Equal("I was called!", ex.Message);
        }

        [Fact]
        public async Task TestReturnsAsyncTask()
        {
            var howler = new Howler();
            await howler.Invoke(ExampleStaticClass.ReturnAsync);
            Assert.True(ExampleStaticClass.Check);
        }

        [Fact]
        public void SimpleUsageExample()
        {
            //real
            var howler = new Howler();

            var sut = new ExampleConsumerClass(howler);
            var result = sut.SimpleMethod("Hello");
            Assert.Equal(5, result);

            //Proxied
            var howlerProxy = new Proxy<IHowler>();
            howlerProxy.Setup(x => x.Invoke(() => ExampleStaticClass.ReturnLength(Any<string>.Value)), () => 15);

            var sut2 = new ExampleConsumerClass(howlerProxy.Object);
            var result2 = sut2.SimpleMethod("Hello");
            Assert.Equal(15, result2);
        }

        [Fact]
        public async Task ComplexUsageExample()
        {
            //real
            var howler = new Howler();

            var sut = new ExampleConsumerClass(howler);
            var result = await sut.ComplexMethod("Hello");
            Assert.Equal(30, result);

            //Proxied
            var howlerProxy = new Proxy<IHowler>();
            howlerProxy.Setup(x => x.Invoke(() => ExampleStaticClass.ReturnLength(Any<string>.Value)), () => 3);
            howlerProxy.Setup(x => x.Invoke(() => ExampleStaticClass2.ReturnLength(Any<string>.Value)), () => 2);
            howlerProxy.Setup(x => x.Invoke(() => ExampleStaticClass.ReturnLengthAsync(Any<string>.Value)), () => Task.FromResult(4));

            var sut2 = new ExampleConsumerClass(howlerProxy.Object);
            var result2 = await sut2.ComplexMethod("Hello");
            Assert.Equal(10, result2);
        }

    }

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
            var x = await _howler.Invoke(() => ExampleStaticClass.ReturnLengthAsync("hello"));
            var z = _howler.Invoke(() => ExampleStaticClass.ReturnLength(s));
            return z + x * y;
        }
    }

    public static class ExampleStaticClass
    {
        public static bool Check { get; set; }
        public static int ReturnLength(string s) => s.Length;
        public static async Task<int> ReturnLengthAsync(string s) => await Task.FromResult(s.Length);
        public static async Task ReturnAsync()
        {
            Check = true;
            await Task.CompletedTask;
        }

        public static void VoidLength() => throw new Exception("I was called!");
    } 
    
    public static class ExampleStaticClass2
    {
        public static int ReturnLength(string s) => s.Length;
    }

    public class Howler : IHowler
    {
        public TResult Invoke<TResult>(Func<TResult> original) => original.Invoke();

        public void Invoke(Action original) => original.Invoke();

        public Task<TResult> Invoke<TResult>(Func<Task<TResult>> original) => original.Invoke();

        public Task Invoke(Func<Task> original) => original.Invoke();

    }

    public interface IHowler
    {
        TResult Invoke<TResult>(Func<TResult> original);
        void Invoke(Action original);
        Task<TResult> Invoke<TResult>(Func<Task<TResult>> original);
        Task Invoke(Func<Task> original);
    }
}