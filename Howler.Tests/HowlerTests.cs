using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Howler.Tests
{
    public class HowlerTests
    {
        public HowlerTests()
        {
            HowlerStructures.AllStructures();
        }

        #region TryCatch
        [Fact]
        public void TestReturnsThroughTryCatch_Pass()
        {
            var howler = new Howler();
            var result = howler.Invoke(() => ExampleStaticClass.ReturnLength("hey"), HowlerStructures.TryCatchStructureId);
            Assert.Equal(3, result);
        }

        [Fact]
        public void TestReturnsThroughTryCatch_Fail()
        {
            var howler = new Howler();
            var result = Assert.Throws<InvalidOperationException>(() => howler.Invoke(() => ExampleStaticClass.ReturnLength(null), HowlerStructures.TryCatchStructureId));
            Assert.StartsWith("uh-oh", result.Message);
        }
        #endregion

        #region Events

        private string _hello;

        [Fact]
        public async Task TestEvent_Simple()
        {
            var howler = new Howler();

            HowlerStructures.SayHello += SayHello;

            await howler.Invoke(() => ExampleStaticClass.ReturnLengthAsync("hey"), HowlerStructures.SayHelloRaisingStructureId);
            Assert.Equal("hello", _hello);

            HowlerStructures.SayHello -= SayHello;
        }

        private void SayHello(object? sender, EventArgs e)
        {
            _hello = "hello";
        }

        private readonly List<string> _db = new();

        [Fact]
        public async Task TestEvent_AddToDb()
        {
            var howler = new Howler();
            HowlerStructures.AddToDb += AddHelloToDb;

            await howler.Invoke(() => ExampleStaticClass.ReturnLengthAsync("hey"), HowlerStructures.AddToDbRaisingStructureId);
            Assert.Single(_db);
            Assert.Equal("Hello!", _db[0]);

            HowlerStructures.AddToDb -= AddHelloToDb;
        }
        private void AddHelloToDb(object? sender, AddToDbEventArgs e)
        {
            _db.Add(e.Hello);
        }

        #endregion

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
            await howler.Invoke(() => ExampleStaticClass.ReturnAsync());
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

            //Registered
            var howlerIntercept = new InTestHowler();
            howlerIntercept.Register(() => ExampleStaticClass.ReturnLength(A<string>.Value), () => 15);
            var sut2 = new ExampleConsumerClass(howlerIntercept);
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

            //Registered
            var howlerIntercept = new InTestHowler();
            howlerIntercept.Register(() => ExampleStaticClass.ReturnLength(A<string>.Value), () => 3);
            howlerIntercept.Register(() => ExampleStaticClass2.ReturnLength(A<string>.Value), () => 2);
            howlerIntercept.Register(() => ExampleStaticClass.ReturnLengthAsync(A<string>.Value), () => Task.FromResult(4));

            var sut2 = new ExampleConsumerClass(howlerIntercept);
            var result2 = await sut2.ComplexMethod("Hello");
            Assert.Equal(11, result2);
        }

        [Fact]
        public void CallbackExample()
        {
            var howlerIntercept = new InTestHowler();

            var exampleDb = new List<string>();
            howlerIntercept.Register(() => ExampleStaticClass.ReturnLength(A<string>.Value), () =>
            {
                exampleDb.Add("hi");
                return 15;
            });

            var sut = new ExampleConsumerClass(howlerIntercept);
            var result = sut.SimpleMethod("Hello");
            Assert.Equal(15, result);

            Assert.Single(exampleDb);
            Assert.Equal("hi", exampleDb.Single());
        }

        [Fact]
        public void InstanceClassExample()
        {
            var howlerIntercept = new InTestHowler();

            howlerIntercept.Register<ExampleInstanceClass, int>(x => x.Three(), () => 5);

            var sut = new ExampleConsumerClass(howlerIntercept);
            var result = sut.CallInstance();
            Assert.Equal(5, result);
        }

        [Fact]
        public void PrimitiveExample()
        {
            var howlerIntercept = new InTestHowler();
            howlerIntercept.Register<int, bool>(x => x.Equals(x + 1), () => true);

            Assert.False(2.Equals(3));
            Assert.True(howlerIntercept.Invoke(() => 2.Equals(3)));
        }
    }
}