using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Howler.Tests.Objects;
using Howler.Tests.Objects.ExampleObjects;
using Howler.Tests.Objects.StructureExamples;
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
            var result = howler.Invoke(() => ExampleStaticClass.ReturnLength("hey"), StructuresIds.TryCatchStructureId);
            Assert.Equal(3, result);
        }

        [Fact]
        public void TestReturnsThroughTryCatch_Fail()
        {
            var howler = new Howler();
            var result = Assert.Throws<InvalidOperationException>(() => howler.Invoke(() => ExampleStaticClass.ReturnLength(null), StructuresIds.TryCatchStructureId));
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

            await howler.Invoke(() => ExampleStaticClass.ReturnLengthAsync("hey"), StructuresIds.SayHelloRaisingStructureId);
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

            await howler.Invoke(() => ExampleStaticClass.ReturnLengthAsync("hey"), StructuresIds.AddToDbRaisingStructureId);
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
            var ex = Assert.Throws<Exception>(() => howler.InvokeVoid(ExampleStaticClass.VoidLength));
            Assert.Equal("I was called!", ex.Message);
        }

        [Fact]
        public async Task TestReturnsAsyncTask()
        {
            var howler = new InTestHowler();
            await howler.Invoke(ExampleStaticClass.ReturnAsync);
            Assert.True(ExampleStaticClass.Check);
        }

        [Fact]
        public void TestReturns()
        {
            var howler = new InTestHowler();
            howler.Register(() => ExampleStaticClass.ReturnLength("hello"), () => 35);
            var res1 = howler.Invoke(() => ExampleStaticClass.ReturnLength("hello"));
            Assert.Equal(35, res1);
            
            howler.Register(() => ExampleStaticClass.ReturnLength("hello"), () => 3);
            var res2 = howler.Invoke(() => ExampleStaticClass.ReturnLength("hello"));
            Assert.Equal(3, res2); 

            howler.Register<int, string>(() => ExampleStaticClass.ReturnLength("hello"), x => x.Length + 3,Guid.Empty);
            var res23 = howler.Invoke(() => ExampleStaticClass.ReturnLength("hello"), Guid.Empty);
            Assert.Equal(8, res23);
        }

        [Fact]
        public void TestConstReturns()
        {
            var howler = new InTestHowler();
            var x = howler.Invoke(() => "hello my baby" + "don't fall!");
            Assert.True(ExampleStaticClass.Check);
        }


    }
}