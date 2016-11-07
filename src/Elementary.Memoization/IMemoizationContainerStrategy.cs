namespace Elementary.Memoization
{
    using System;

    /// <summary>
    /// Implement this interface to create Memoization container provider.
    /// </summary>
    public interface IMemoizationContainerStrategy
    {
        /// <summary>
        /// Creates a meomization container instance storing instances of type <typeparamref name="V"/> identified by instances of <typeparamref name="K"/>
        /// </summary>
        /// <typeparam name="K">type of the key of the memoization container</typeparam>
        /// <typeparam name="V">typeof os the value stored in the memoization container</typeparam>
        /// <param name="toMemoize"></param>
        /// <returns></returns>
        Func<K, V> CreateNew<K, V>(Func<K, V> toMemoize);
    }
}