namespace Elementary.Memoization.Test
{
    using Elementary.Memoization;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class Memoization_1P_TSweak_Test
    {
        private int toStringCalled = 0;

        private string toString(int from)
        {
            this.toStringCalled++;
            return from.ToString();
        }

        private Func<int, string> memoized;

        [SetUp]
        public void ArrangeAllTests()
        {
            this.toStringCalled = 0;

            this.memoized = new MemoizationBuilder()
                .MapFromParameterTuples()
                .StoreInDictionaryWithWeakReferences()
                .From<int, string>(this.toString);
        }

        [Test]
        public void Memoization_1P_TSweak_function_is_called()
        {
            // ACT

            string result1 = this.memoized(1);

            // ASSERT

            Assert.AreEqual("1", result1);
            Assert.AreEqual(1, toStringCalled);
        }

        [Test]
        public void Memoization_1P_TSweak_function_is_not_called_twice_for_same_parameter()
        {
            // ARRANGE

            var valueOf1 = this.memoized(1);

            // ACT

            string result = this.memoized(1);

            // ASSERT

            Assert.AreSame(valueOf1, result);
            Assert.AreEqual(1, toStringCalled);
        }

        [Test]
        public void Memoization_1P_TSweak_function_is_called_again_for_different_parameter()
        {
            // ARRANGE

            var valueOf1 = this.memoized(1);

            // ACT

            string result = this.memoized(2);

            // ASSERT

            Assert.AreEqual("2", result);
            Assert.AreEqual(2, toStringCalled);
        }
    }
}