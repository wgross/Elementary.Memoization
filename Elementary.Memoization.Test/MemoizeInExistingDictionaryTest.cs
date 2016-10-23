namespace Elementary.Memoization.Test
{
    using Elementary.Memoization;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class MemoizeInExistingDictionaryTest
    {
        private int toStringCalled;

        private string toString(int p1)
        {
            this.toStringCalled++;
            return p1.ToString();
        }

        private string toString2(int p1, int p2)
        {
            this.toStringCalled++;
            return string.Join(",", p1.ToString(), p2.ToString());
        }

        [SetUp]
        public void ArrangeAllTests()
        {
            this.toStringCalled = 0;
        }

        [Test]
        public void Memoize_1P_in_existing_dictionary()
        {
            // ARRANGE

            Dictionary<int, string> memoizationContainer = new Dictionary<int, string>();

            // ACT

            Func<int, string> memoized = new MemoizationBuilder()
                .MapFromParameterTuples()
                .StoreInExternalDictionaryWithStrongReferences(memoizationContainer)
                .From<int, string>(toString);

            // ASSERT

            Assert.IsNotNull(memoized);
        }

        //[Test]
        //public void Memoize_1P_in_existing_dictionary_alternative()
        //{
        //    // ARRANGE

        //    Dictionary<int, string> memoizationContainer = new Dictionary<int, string>();

        //    // ACT

        //    Func<int, string> memoized = Memoize.From<int, string>(toString, Memoize.WithTuples.AndFactory(delegate(Func<int, string> toMemoize)
        //    {
        //        var tmp = new Dictionary<int, string>();
        //        return p1 =>
        //        {
        //            return

        //        }
        //    }
        //        ));

        //    // ASSERT

        //    Assert.IsNotNull(memoized);
        //}

        [Test]
        public void Memoize_1P_in_existing_dictionary_remembers_result()
        {
            // ARRANGE

            Dictionary<int, string> memoizationContainer = new Dictionary<int, string>();

            Func<int, string> memoized = new MemoizationBuilder()
                .MapFromParameterTuples()
                .StoreInExternalDictionaryWithStrongReferences(memoizationContainer)
                .From<int, string>(toString);

            // ACT

            memoized(1);

            // ASSERT
            Assert.AreEqual(1, this.toStringCalled);
            Assert.AreEqual(1, memoizationContainer.Count);
            Assert.AreEqual("1", memoizationContainer.Values.Single());
        }

        [Test]
        public void Memoize_2P_in_existing_dictionary()
        {
            // ARRANGE

            var memoizationContainer = new Dictionary<Tuple<int, int>, string>();

            // ACT

            Func<int, int, string> memoized = new MemoizationBuilder()
                .MapFromParameterTuples()
                .StoreInExternalDictionaryWithStrongReferences(memoizationContainer)
                .From<int, int, string>(toString2);

            // ASSERT

            Assert.IsNotNull(memoized);
        }

        [Test]
        public void Memoize_2P_in_existing_dictionary_remembers_result()
        {
            // ARRANGE

            var memoizationContainer = new Dictionary<Tuple<int, int>, string>();
            var memoized = new MemoizationBuilder()
                .MapFromParameterTuples()
                .StoreInExternalDictionaryWithStrongReferences(memoizationContainer)
                .From<int, int, string>(toString2);

            // ACT

            memoized(1, 2);

            // ASSERT

            Assert.AreEqual(1, this.toStringCalled);
            Assert.AreEqual("1,2", memoizationContainer.Values.Single());
            Assert.AreEqual(Tuple.Create(1, 2), memoizationContainer.Keys.Single());
        }

        [Test]
        public void Memoize_2P_in_existing_dictionary_calculateas_after_clear()
        {
            // ARRANGE

            var memoizationContainer = new Dictionary<Tuple<int, int>, string>();
            var memoized = new MemoizationBuilder()
                .MapFromParameterTuples()
                .StoreInExternalDictionaryWithStrongReferences(memoizationContainer)
                .From<int, int, string>(toString2);

            memoized(1, 2);
            memoizationContainer.Clear();

            // ACT

            memoized(1, 2);

            // ASSERT

            Assert.AreEqual(2, this.toStringCalled);
            Assert.AreEqual("1,2", memoizationContainer.Values.Single());
            Assert.AreEqual(Tuple.Create(1, 2), memoizationContainer.Keys.Single());
        }

        [Test]
        public void Memoize_2P_in_existing_dictionary_with_wromg_type_fails()
        {
            // ARRANGE

            var memoizationContainer = new Dictionary<int, string>();

            // ACT

            Assert.Throws<InvalidCastException>(() =>
            {
                Func<int, int, string> memoized = new MemoizationBuilder()
                    .MapFromParameterTuples()
                    .StoreInExternalDictionaryWithStrongReferences(memoizationContainer)
                    .From<int, int, string>(toString2);
            });
        }
    }
}