namespace Elementary.Memoization
{
    using System;

    /// <summary>
    /// Implement this interface to define how a memoization of the given delegate and the given container provider is built.
    /// </summary>
    public interface IMemoizationDelegateFactory
    {
        Func<P1, P2, P3, P4, P5, P6, P7, R> From<P1, P2, P3, P4, P5, P6, P7, R>(Func<P1, P2, P3, P4, P5, P6, P7, R> toMemoize, IMemoizationContainerStrategy memoizationContainerProvider);

        Func<P1, P2, P3, P4, P5, P6, R> From<P1, P2, P3, P4, P5, P6, R>(Func<P1, P2, P3, P4, P5, P6, R> toMemoize, IMemoizationContainerStrategy memoizationContainerProvider);

        Func<P1, P2, P3, P4, P5, R> From<P1, P2, P3, P4, P5, R>(Func<P1, P2, P3, P4, P5, R> toMemoize, IMemoizationContainerStrategy memoizationContainerProvider);

        Func<P1, P2, P3, P4, R> From<P1, P2, P3, P4, R>(Func<P1, P2, P3, P4, R> toMemoize, IMemoizationContainerStrategy memoizationContainerProvider);

        Func<P1, P2, P3, R> From<P1, P2, P3, R>(Func<P1, P2, P3, R> toMemoize, IMemoizationContainerStrategy memoizationContainerProvider);

        Func<P1, P2, R> From<P1, P2, R>(Func<P1, P2, R> toMemoize, IMemoizationContainerStrategy memoizationContainerProvider);

        Func<P1, R> From<P1, R>(Func<P1, R> toMemoize, IMemoizationContainerStrategy memoizationContainerProvider);
    }
}