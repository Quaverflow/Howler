using System;
using System.Threading.Tasks;
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
            var x = await howler.InvokeAsync(() => ExampleStaticClass.ReturnLengthAsync("hey"));
            Assert.Equal(3, x);
        }

        [Fact]
        public void TestVoid()
        {
            var howler = new Howler();
            var ex = Assert.Throws<Exception>(() => howler.InvokeVoid(() => ExampleStaticClass.VoidLength()));
            Assert.Equal("I was called!", ex.Message);
        }

        [Fact]
        public async Task TestReturnsAsyncTask()
        {
            var howler = new Howler();
            await howler.InvokeTask(() => ExampleStaticClass.ReturnAsync());
            Assert.True(ExampleStaticClass.Check);
        }

        [Fact]
        public void SimpleUsageExample()
        {
            //real
            var howler = new InTestHowler();

            var sut = new ExampleConsumerClass(howler);
            var result = sut.SimpleMethod("Hello");
            Assert.Equal(5, result);

            //Registered
            howler.Register(() => ExampleStaticClass.ReturnLength(A<string>.Value), () => 15);
            var sut2 = new ExampleConsumerClass(howler);
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
            var howlerIntercept = new InTestHowler();
            howlerIntercept.Register(() => ExampleStaticClass.ReturnLength(A<string>.Value), () => 3);
            howlerIntercept.Register(() => ExampleStaticClass2.ReturnLength(A<string>.Value), () => 2);
            howlerIntercept.Register(() => ExampleStaticClass.ReturnLengthAsync(A<string>.Value), () => Task.FromResult(4));

            var sut2 = new ExampleConsumerClass(howlerIntercept);
            var result2 = await sut2.ComplexMethod("Hello");
            Assert.Equal(11, result2);
        }



    }
}