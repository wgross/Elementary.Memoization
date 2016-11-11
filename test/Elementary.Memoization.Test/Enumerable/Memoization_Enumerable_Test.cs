using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Elementary.Memoization.Test.Enumerable
{
    [TestFixture]
    public class Memoization_Enumerable_Test
    {
        [Test]
        public void Builder_creates_Enumerable()
        {
            // ARRANGE

            var enumerable = new Mock<IEnumerable<int>>();

            // ACT

            var result = new MemoizationBuilder().From(enumerable.Object);

            // ASSERT

            Assert.IsNotNull(result);
            Assert.AreNotSame(result, enumerable);
        }

        [Test]
        public void Memoized_Enumerator_calls_sources_MoveNext()
        {
            // ARRANGE

            var enumerator = new Mock<IEnumerator<int>>();
            enumerator.Setup(e => e.MoveNext()).Returns(true);

            var enumerable = new Mock<IEnumerable<int>>();

            enumerable.Setup(e => e.GetEnumerator()).Returns(enumerator.Object);

            var memoized = new MemoizationBuilder().From(enumerable.Object);

            // ACT

            var result = memoized.GetEnumerator().MoveNext();

            // ASSERT

            Assert.IsTrue(result);

            enumerable.Verify(e => e.GetEnumerator(), Times.Once());
            enumerable.VerifyAll();
            enumerator.Verify(e => e.MoveNext(), Times.Once());
            enumerator.VerifyAll();
        }

        [Test]
        public void Memoized_Enumerator_calls_sources_Current()
        {
            // ARRANGE

            var enumerator = new Mock<IEnumerator<int>>();
            enumerator.Setup(e => e.MoveNext()).Returns(true);
            enumerator.SetupGet(e => e.Current).Returns(3);

            var enumerable = new Mock<IEnumerable<int>>();

            enumerable.Setup(e => e.GetEnumerator()).Returns(enumerator.Object);

            var memoized = new MemoizationBuilder().From(enumerable.Object);

            // ACT

            var result = memoized.First();

            // ASSERT

            Assert.AreEqual(3, result);

            enumerable.Verify(e => e.GetEnumerator(), Times.Once());
            enumerable.VerifyAll();
            enumerator.Verify(e => e.MoveNext(), Times.Once());
            enumerator.Verify(e => e.Current, Times.Once());
            enumerator.VerifyAll();
        }

        [Test]
        public void Memoized_Enumerator_calls_sources_Current_only_once()
        {
            // ARRANGE

            var firstItem = "item";
            var enumerator = new Mock<IEnumerator<string>>();
            enumerator.Setup(e => e.MoveNext()).Returns(true);
            enumerator.SetupGet(e => e.Current).Returns(firstItem);

            var enumerable = new Mock<IEnumerable<string>>();

            enumerable.Setup(e => e.GetEnumerator()).Returns(enumerator.Object);

            var memoized = new MemoizationBuilder().From(enumerable.Object);
            var firstResult = memoized.First();

            // ACT

            var result = memoized.First();

            // ASSERT

            Assert.AreSame(firstResult, result);

            enumerable.Verify(e => e.GetEnumerator(), Times.Once());
            enumerable.VerifyAll();
            enumerator.Verify(e => e.MoveNext(), Times.Once());
            enumerator.Verify(e => e.Current, Times.Once());
            enumerator.VerifyAll();
        }

        [Test]
        public void Memoized_Enumerator_calls_sources_Current_on_complete_enumeration_2()
        {
            // ARRANGE

            var innerList = new List<string>() { "item1", "item2" };
            int called = 0;
            var innerEnumerable = innerList.Select(i => { called++; return i; });
            var memoized = new MemoizationBuilder().From(innerEnumerable);
            var firstResult = memoized.First();

            // ACT

            var result = memoized.ToArray();

            // ASSERT

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("item1", firstResult);
            Assert.AreEqual("item1", result[0]);
            Assert.AreEqual("item2", result[1]);
            Assert.AreEqual(2, called);
        }
    }
}