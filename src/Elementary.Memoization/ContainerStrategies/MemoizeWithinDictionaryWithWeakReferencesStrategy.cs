namespace Elementary.Memoization.ContainerStrategies
{
    using System;
    using System.Collections.Generic;

    internal sealed class MemoizeWithinDictionaryWithWeakReferencesStrategy : IMemoizationContainerStrategy
    {
        public Dictionary<K, V> CreateDictionary<K, V>()
        {
            return new Dictionary<K, V>();
        }

        public Func<K, V> CreateNew<K, V>(Func<K, V> toMemoize)
        {
            var memoizationContainer = this.CreateDictionary<K, WeakReference>();

            return key =>
            {
                WeakReference weakReference;
                if (memoizationContainer.TryGetValue(key, out weakReference))
                    if (weakReference.IsAlive)
                        return (V)(weakReference.Target);

                V value = toMemoize(key);
                memoizationContainer.Add(key, new WeakReference(value));
                return value;
            };
        }
    }
}