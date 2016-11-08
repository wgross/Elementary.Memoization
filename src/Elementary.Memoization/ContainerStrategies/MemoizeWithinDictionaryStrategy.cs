namespace Elementary.Memoization.ContainerStrategies
{
    using System;
    using System.Collections.Generic;

    internal sealed class MemoizeWithinDictionaryStrategy : IMemoizationContainerStrategy
    {
        public Func<K, V> CreateNew<K, V>(Func<K, V> toMemoize)
        {
            var memoizationContainer = new Dictionary<K, V>();

            return key =>
            {
                V value;
                if (memoizationContainer.TryGetValue(key, out value))
                    return value;

                memoizationContainer.Add(key, value = toMemoize(key));
                return value;
            };
        }
    }
}