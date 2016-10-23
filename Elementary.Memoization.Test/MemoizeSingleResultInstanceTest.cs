namespace Elementary.Memoization.Test
{
    using Elementary.Memoization;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class MemoizeSingleResultInstanceTest
    {
        private Func<int, string> memoized;

        private int toStringCalled = 0;

        private string toString(int from)
        {
            this.toStringCalled++;
            return from.ToString();
        }

        [SetUp]
        public void ArrangeAllTests()
        {
            this.toStringCalled = 0;

            this.memoized = new MemoizationBuilder()
                .MapFromParameterTuples()
                .StoreLatestResultOnlyWithStrongReferences()
                .From<int, string>(this.toString);
        }

        [Test]
        public void SingleResultIsNotCalculatedTwice()
        {
            // ARRANGE

            var firstCall = this.memoized(1);

            // ACT

            string result1 = this.memoized(1);

            // ASSERT

            Assert.AreEqual("1", result1);
            Assert.AreSame(firstCall, result1);
        }

        [Test]
        public void SingleResultRecalculatedIfCallParametersHadChanged()
        {
            // ARRANGE

            var firstCall = this.memoized(1);
            var secindCall = this.memoized(2);

            // ACT

            string result1 = this.memoized(1);

            // ASSERT

            Assert.AreEqual("1", result1);
            Assert.AreNotSame(firstCall, result1);
        }
    }
}