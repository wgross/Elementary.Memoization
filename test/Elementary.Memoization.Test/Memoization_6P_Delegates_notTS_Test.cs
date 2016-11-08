namespace Elementary.Memoization.Test
{
    using Elementary.Memoization;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class Memoization_6P_Delegates_notTS_Test
    {
        private int toStringCalled = 0;

        private string toString(int from1, int from2, int from3, int from4, int from5, int from6)
        {
            this.toStringCalled++;
            return string.Join(":", from1.ToString(), from2.ToString(), from3.ToString(), from4.ToString(), from5.ToString(), from6.ToString());
        }

        private Func<int, int, int, int, int, int, string> memoized;

        [SetUp]
        public void ArrangeAllTests()
        {
            this.toStringCalled = 0;

            this.memoized = new MemoizationBuilder()
                .UsingCurrying()
                .WithinDictionary()
                .From<int, int, int, int, int, int, string>(this.toString);
        }

        [Test]
        public void Memoization_6P_Delegates_notTS_function_is_called_for_unknown_parameters()
        {
            // ACT & ASSERT

            Assert.AreEqual("2:1:1:1:1:1", this.memoized(2, 1, 1, 1, 1, 1));
            Assert.AreEqual("1:2:1:1:1:1", this.memoized(1, 2, 1, 1, 1, 1));
            Assert.AreEqual("1:1:2:1:1:1", this.memoized(1, 1, 2, 1, 1, 1));
            Assert.AreEqual("1:1:1:2:1:1", this.memoized(1, 1, 1, 2, 1, 1));
            Assert.AreEqual("1:1:1:1:2:1", this.memoized(1, 1, 1, 1, 2, 1));
            Assert.AreEqual("1:1:1:1:1:2", this.memoized(1, 1, 1, 1, 1, 2));
            Assert.AreEqual(6, this.toStringCalled);
        }

        [Test]
        public void Memoization_6P_Delegates_notTS_function_is_called_twice_for_known_parameters()
        {
            // ARRANGE

            this.memoized(2, 1, 1, 1, 1, 1);
            this.memoized(1, 2, 1, 1, 1, 1);
            this.memoized(1, 1, 2, 1, 1, 1);
            this.memoized(1, 1, 1, 2, 1, 1);
            this.memoized(1, 1, 1, 1, 2, 1);
            this.memoized(1, 1, 1, 1, 1, 2);

            // ACT & ASSERT

            Assert.AreEqual("2:1:1:1:1:1", this.memoized(2, 1, 1, 1, 1, 1));
            Assert.AreEqual("1:2:1:1:1:1", this.memoized(1, 2, 1, 1, 1, 1));
            Assert.AreEqual("1:1:2:1:1:1", this.memoized(1, 1, 2, 1, 1, 1));
            Assert.AreEqual("1:1:1:2:1:1", this.memoized(1, 1, 1, 2, 1, 1));
            Assert.AreEqual("1:1:1:1:2:1", this.memoized(1, 1, 1, 1, 2, 1));
            Assert.AreEqual("1:1:1:1:1:2", this.memoized(1, 1, 1, 1, 1, 2));
            Assert.AreEqual(6, this.toStringCalled);
        }
    }
}