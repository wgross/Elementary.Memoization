namespace Elementary.Memoization
{
    using System;

    /// <summary>
    /// Implement this interface to create Memoization container provider.
    /// The container provided mus by able to map the specified instance of K to an instance of V.
    /// </summary>
    public interface IMemoizationContainerStrategy
    {
        Func<K, V> CreateNew<K, V>(Func<K, V> toMemoize);
    }
}