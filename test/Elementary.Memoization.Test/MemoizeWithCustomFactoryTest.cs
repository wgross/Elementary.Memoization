using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elementary.Memoization.Test
{
    [TestFixture]
    public class MemoizeWithCustomFactoryTest
    {
        [Test]
        public void Memoize_fails_with_null_IMemoizationContainerStrategy()
        {
            // ACT & ASSERT

            var result = Assert.Throws<ArgumentNullException>(() => new MemoizationBuilder()
                .UsingTuples()
                .Within((IMemoizationContainerStrategy)null));

            Assert.IsTrue(result.ParamName.Equals("memoizationContainerStrategy"));
        }
    }
}
