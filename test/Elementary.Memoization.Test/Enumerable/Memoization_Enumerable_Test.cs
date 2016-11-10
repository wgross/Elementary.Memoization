using Moq;
using NUnit.Framework;
using System.Collections.Generic;

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
        public void Memoized_Enumerable_creates_Enumerator()
        {
            // ARRANGE

            var enumerator = new Mock<IEnumerator<int>>();
            var enumerable = new Mock<IEnumerable<int>>();

            enumerable.Setup(e => e.GetEnumerator()).Returns(enumerator.Object);

            var memoized = new MemoizationBuilder().From(enumerable.Object);

            // ACT

            var result = memoized.GetEnumerator();

            // ASSERT

            Assert.IsNotNull(result);
            Assert.AreNotSame(result, enumerable);
            enumerable.Verify(e => e.GetEnumerator(), Times.Once());
            enumerable.VerifyAll();
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
            enumerator.SetupGet(e => e.Current).Returns(3);

            var enumerable = new Mock<IEnumerable<int>>();

            enumerable.Setup(e => e.GetEnumerator()).Returns(enumerator.Object);

            var memoized = new MemoizationBuilder().From(enumerable.Object);

            // ACT

            var result = memoized.GetEnumerator().Current;

            // ASSERT

            Assert.AreEqual(3, result);

            enumerable.Verify(e => e.GetEnumerator(), Times.Once());
            enumerable.VerifyAll();
            enumerator.Verify(e => e.Current, Times.Once());
            enumerable.VerifyAll();
        }

        [Test]
        public void Memoized_Enumerator_calls_sources_Reset()
        {
            // ARRANGE

            var enumerator = new Mock<IEnumerator<int>>();

            var enumerable = new Mock<IEnumerable<int>>();

            enumerable.Setup(e => e.GetEnumerator()).Returns(enumerator.Object);

            var memoized = new MemoizationBuilder().From(enumerable.Object);

            // ACT

            memoized.GetEnumerator().Reset();

            // ASSERT

            enumerable.Verify(e => e.GetEnumerator(), Times.Once());
            enumerable.VerifyAll();
            enumerator.Verify(e => e.Reset(), Times.Once());
            enumerable.VerifyAll();
        }

        [Test]
        public void Memoized_Enumerator_calls_sources_Dispose()
        {
            // ARRANGE

            var enumerator = new Mock<IEnumerator<int>>();

            var enumerable = new Mock<IEnumerable<int>>();

            enumerable.Setup(e => e.GetEnumerator()).Returns(enumerator.Object);

            var memoized = new MemoizationBuilder().From(enumerable.Object);

            // ACT

            memoized.GetEnumerator().Dispose();

            // ASSERT

            enumerable.Verify(e => e.GetEnumerator(), Times.Once());
            enumerable.VerifyAll();
            enumerator.Verify(e => e.Dispose(), Times.Once());
            enumerable.VerifyAll();
        }
    }
}