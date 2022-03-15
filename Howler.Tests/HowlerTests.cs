//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Design;
//using System.Threading.Tasks;
//using Howler.Tests.Objects.ExampleObjects;
//using Howler.Tests.Objects.StructureExamples;
//using Xunit;

//namespace Howler.Tests
//{
//    public class HowlerTests
//    {
//        public HowlerTests()
//        {
//            HowlerStructures.AllStructures();
//        }

//        #region TryCatch
//        [Fact]
//        public void TestReturnsThroughTryCatch_Pass()
//        {
//            var howler = new Howler(new ServiceContainer());
//            var result = howler.Invoke(() => ExampleStaticClass.ReturnLength("hey"), StructuresIds.TryCatchStructureId);
//            Assert.Equal(3, result);
//        }

//        [Fact]
//        public void TestReturnsThroughTryCatch_Fail()
//        {
//            var howler = new Howler(new ServiceContainer());
//            var result = Assert.Throws<InvalidOperationException>(() => howler.Invoke(() => ExampleStaticClass.ReturnLength(null), StructuresIds.TryCatchStructureId));
//            Assert.StartsWith("uh-oh", result.Message);
//        }
//        #endregion

//        #region Events

//        private string _hello;

//        [Fact]
//        public async Task TestEvent_Simple()
//        {
//            var howler = new Howler(new ServiceContainer());

//            HowlerStructures.SayHello += SayHello;

//            await howler.Invoke(() => ExampleStaticClass.ReturnLengthAsync("hey"), StructuresIds.SayHelloRaisingStructureId);
//            Assert.Equal("hello", _hello);

//            HowlerStructures.SayHello -= SayHello;
//        }

//        private void SayHello(object? sender, EventArgs e)
//        {
//            _hello = "hello";
//        }

//        private readonly List<string> _db = new();

//        [Fact]
//        public async Task TestEvent_AddToDb()
//        {
//            var howler = new Howler(new ServiceContainer());
//            HowlerStructures.AddToDb += AddHelloToDb;

//            await howler.Invoke(() => ExampleStaticClass.ReturnLengthAsync("hey"), StructuresIds.AddToDbRaisingStructureId);
//            Assert.Single(_db);
//            Assert.Equal("Hello!", _db[0]);

//            HowlerStructures.AddToDb -= AddHelloToDb;
//        }
//        private void AddHelloToDb(object? sender, AddToDbEventArgs e)
//        {
//            _db.Add(e.Hello);
//        }

//        #endregion

//        [Fact]
//        public void TestVoid()
//        {
//            var howler = new Howler(new ServiceContainer());
//            var ex = Assert.Throws<Exception>(() => howler.InvokeVoid(ExampleStaticClass.VoidLength, TestStructuresIds.Structure1));
//            Assert.Equal("I was called!", ex.Message);
//        }

//        [Fact]
//        public async Task TestReturnsAsyncTask()
//        {
//            var howler = new InTestHowler();
//            await howler.Invoke(ExampleStaticClass.ReturnAsync, TestStructuresIds.Structure1);
//            Assert.True(ExampleStaticClass.Check);
//        }

//        [Fact]
//        public void TestReturns()
//        {
//            var howler = new InTestHowler();
//            howler.Register(() => 35, TestStructuresIds.Structure1);
//            var res1 = howler.Invoke(() => ExampleStaticClass.ReturnLength("hello"), TestStructuresIds.Structure1);
//            Assert.Equal(35, res1);

//        }

//        [Fact]
//        public void TestReturns2()
//        {
//            var howler = new InTestHowler();
//            howler.Register(() => 3, TestStructuresIds.Structure1);
//            var res2 = howler.Invoke(() => ExampleStaticClass.ReturnLength("hello"), TestStructuresIds.Structure1);
//            Assert.Equal(3, res2);
//        }

//        [Fact]
//        public void TestReturns3()
//        {
//            var howler = new InTestHowler();
//            howler.Register<string, int>(x => x.Length + 3, TestStructuresIds.Structure1);
//            var res23 = howler.Invoke(() => ExampleStaticClass.ReturnLength("hello"), TestStructuresIds.Structure1);
//            Assert.Equal(8, res23);
//        }



//        [Fact]
//        public void TestReturns4()
//        {
//            var howler = new InTestHowler();
//            howler.Register<string, int, int>((x, y) => x.Length + y + 3, TestStructuresIds.Structure1);

//            var res223 = howler.Invoke(() => ExampleStaticClass.ReturnLength("hello", 10), TestStructuresIds.Structure1);
//            Assert.Equal(18, res223);
//        }

//        [Fact]
//        public void TestConstReturns()
//        {
//            var howler = new InTestHowler();
//            var x = howler.Invoke(() => "hello my baby" + "don't fall!", TestStructuresIds.Structure1);
//            Assert.True(ExampleStaticClass.Check);
//        }


//    }
//}