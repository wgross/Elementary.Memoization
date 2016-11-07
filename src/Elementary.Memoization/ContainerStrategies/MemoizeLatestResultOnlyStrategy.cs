namespace Elementary.Memoization.ContainerStrategies
{
    using System;

    internal sealed class MemoizeLatestResultOnlyStrategy : IMemoizationContainerStrategy
    {
        public Func<K, V> CreateNew<K, V>(Func<K, V> toMemoize)
        {
            Tuple<K,V> memoizationContainer = null;

            return key =>
            {
                if(memoizationContainer!=null && memoizationContainer.Item1.Equals(key))
                    return memoizationContainer.Item2;

                V value;
                memoizationContainer = Tuple.Create(key, value = toMemoize(key));
                return value;
            };
        }
    }
}