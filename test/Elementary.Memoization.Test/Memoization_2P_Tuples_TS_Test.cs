namespace Elementary.Memoization.Test
{
    using Elementary.Memoization;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class Memoization_2P_Tuples_TS_Test
    {
        private int toStringCalled = 0;

        private string toString(int from1, int from2)
        {
            this.toStringCalled++;
            return from1.ToString() + ":" + from2.ToString();
        }

        private Func<int, int, string> memoized;

        [SetUp]
        public void ArrangeAllTests()
        {
            this.toStringCalled = 0;
            this.memoized = new MemoizationBuilder()
                .MapFromParameterTuples()
                .StoreInConcurrentDictionaryWithStrongReferences()
                .From<int, int, string>(this.toString);
        }

        [Test]
        public void Memoization_2P_Tuples_notTS_function_is_called_for_unknown_parameters()
        {
            // ACT & ASSERT

            Assert.AreEqual("1:2", this.memoized(1, 2));
            Assert.AreEqual("2:1", this.memoized(2, 1));
            Assert.AreEqual(2, toStringCalled);
        }

        [Test]
        public void Memoization_2P_Tuples_notTS_function_is_not_called_fown_known_parameters()
        {
            // ARRANGE

            this.memoized(1, 2);
            this.memoized(2, 1);

            // ACT & ASSERT

            Assert.AreEqual("1:2", this.memoized(1, 2));
            Assert.AreEqual("2:1", this.memoized(2, 1));
            Assert.AreEqual(2, toStringCalled);
        }
    }
}