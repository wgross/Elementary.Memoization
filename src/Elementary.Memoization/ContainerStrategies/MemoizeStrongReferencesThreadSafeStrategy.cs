namespace Elementary.Memoization.ContainerStrategies
{
    using System;
    using System.Collections.Concurrent;

    internal sealed class MemoizeStrongReferencesThreadSafeStrategy : IMemoizationContainerStrategy
    {
        public Func<K, V> CreateNew<K, V>(Func<K, V> toMemoize)
        {
            var memoizationContainer = new ConcurrentDictionary<K, V>();

            return key =>
            {
                return memoizationContainer.GetOrAdd(key, toMemoize);
            };
        }
    }
}