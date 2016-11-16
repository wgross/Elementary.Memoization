namespace Elementary.Memoization.ContainerStrategies
{
    using System;
    using System.Collections.Generic;

    internal sealed class MemoizeWithinProvidedDictionaryStrategy : IMemoizationContainerStrategy
    {
        public MemoizeWithinProvidedDictionaryStrategy(object dictionaryToUse)
        {
            if (dictionaryToUse == null)
                throw new ArgumentNullException(nameof(dictionaryToUse));

            this.dictionary = dictionaryToUse;
        }

        private readonly object dictionary;

        public Func<K, V> CreateNew<K, V>(Func<K, V> toMemoize)
        {
            var tmp = (IDictionary<K, V>)this.dictionary;

            return p =>
            {
                V value;
                if (!tmp.TryGetValue(p, out value))
                {
                    value = toMemoize(p);
                    tmp.Add(p, value);
                }

                return value;
            };
        }
    }
}