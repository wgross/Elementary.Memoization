namespace Elementary.Memoization
{
    using System;

    /// <summary>
    /// Implement this interface to define how a memoization of the given delegate and the given container provider is built.
    /// </summary>
    public interface IMemoizationDelegateFactory
    {
        /// <summary>
        /// Creates a memoization delegate from a function receiving seven parameters
        /// </summary>
        /// <typeparam name="P1">Type of first parameter</typeparam>
        /// <typeparam name="P2">Type of second parameter</typeparam>
        /// <typeparam name="P3">Type of third parameter</typeparam>
        /// <typeparam name="P4">Type of fourth parameter</typeparam>
        /// <typeparam name="P5">Type of fifth parameter</typeparam>
        /// <typeparam name="P6">Type of sixth parameter</typeparam>
        /// <typeparam name="P7">Type of seventh parameter</typeparam>
        /// <typeparam name="R">Type of result</typeparam>
        /// <param name="toMemoize">Function to memoize results of</param>
        /// <param name="memoizationContainerProvider">provider of a memoization storage</param>
        /// <returns>A memoizing proxy delegate for<paramref name="toMemoize"/></returns>
        Func<P1, P2, P3, P4, P5, P6, P7, R> From<P1, P2, P3, P4, P5, P6, P7, R>(Func<P1, P2, P3, P4, P5, P6, P7, R> toMemoize, IMemoizationContainerStrategy memoizationContainerProvider);

        /// <summary>
        /// Creates a memoization delegate from a function receiving six parameters
        /// </summary>
        /// <typeparam name="P1">Type of first parameter</typeparam>
        /// <typeparam name="P2">Type of second parameter</typeparam>
        /// <typeparam name="P3">Type of third parameter</typeparam>
        /// <typeparam name="P4">Type of fourth parameter</typeparam>
        /// <typeparam name="P5">Type of fifth parameter</typeparam>
        /// <typeparam name="P6">Type of sixth parameter</typeparam>
        /// <typeparam name="R">Type of result</typeparam>
        /// <param name="memoizationContainerProvider">provider of a memoization storage</param>
        /// <param name="toMemoize">Function to memoize results of</param>
        /// <returns>A memoizing proxy delegate for<paramref name="toMemoize"/></returns>
        Func<P1, P2, P3, P4, P5, P6, R> From<P1, P2, P3, P4, P5, P6, R>(Func<P1, P2, P3, P4, P5, P6, R> toMemoize, IMemoizationContainerStrategy memoizationContainerProvider);

        /// <summary>
        /// Creates a memoization delegate from a function receiving five parameters
        /// </summary>
        /// <typeparam name="P1">Type of first parameter</typeparam>
        /// <typeparam name="P2">Type of second parameter</typeparam>
        /// <typeparam name="P3">Type of third parameter</typeparam>
        /// <typeparam name="P4">Type of fourth parameter</typeparam>
        /// <typeparam name="P5">Type of fifth parameter</typeparam>
        /// <typeparam name="R">Type of result</typeparam>
        /// <param name="toMemoize">Function to memoize results of</param>
        /// <param name="memoizationContainerProvider">provider of a memoization storage</param>
        /// <returns>A memoizing proxy delegate for<paramref name="toMemoize"/></returns>
        Func<P1, P2, P3, P4, P5, R> From<P1, P2, P3, P4, P5, R>(Func<P1, P2, P3, P4, P5, R> toMemoize, IMemoizationContainerStrategy memoizationContainerProvider);

        /// <summary>
        /// Creates a memoization delegate from a function receiving four parameters
        /// </summary>
        /// <typeparam name="P1">Type of first parameter</typeparam>
        /// <typeparam name="P2">Type of second parameter</typeparam>
        /// <typeparam name="P3">Type of third parameter</typeparam>
        /// <typeparam name="P4">Type of fourth parameter</typeparam>
        /// <typeparam name="R">Type of result</typeparam>
        /// <param name="toMemoize">Function to memoize results of</param>
        /// <param name="memoizationContainerProvider">provider of a memoization storage</param>
        /// <returns>A memoizing proxy delegate for<paramref name="toMemoize"/></returns>
        Func<P1, P2, P3, P4, R> From<P1, P2, P3, P4, R>(Func<P1, P2, P3, P4, R> toMemoize, IMemoizationContainerStrategy memoizationContainerProvider);

        /// <summary>
        /// Creates a memoization delegate from a function receiving three parameters
        /// </summary>
        /// <typeparam name="P1">Type of first parameter</typeparam>
        /// <typeparam name="P2">Type of second parameter</typeparam>
        /// <typeparam name="P3">Type of third parameter</typeparam>
        /// <typeparam name="R">Type of result</typeparam>
        /// <param name="toMemoize">Function to memoize results of</param>
        /// <param name="memoizationContainerProvider">provider of a memoization storage</param>
        /// <returns>A memoizing proxy delegate for<paramref name="toMemoize"/></returns>
        Func<P1, P2, P3, R> From<P1, P2, P3, R>(Func<P1, P2, P3, R> toMemoize, IMemoizationContainerStrategy memoizationContainerProvider);

        /// <summary>
        /// Creates a memoization delegate from a function receiving two parameters
        /// </summary>
        /// <typeparam name="P1">Type of first parameter</typeparam>
        /// <typeparam name="P2">Type of second parameter</typeparam>
        /// <typeparam name="R">Type of result</typeparam>
        /// <param name="toMemoize">Function to memoize results of</param>
        /// <param name="memoizationContainerProvider">provider of a memoization storage</param>
        /// <returns>A memoizing proxy delegate for<paramref name="toMemoize"/></returns>
        Func<P1, P2, R> From<P1, P2, R>(Func<P1, P2, R> toMemoize, IMemoizationContainerStrategy memoizationContainerProvider);

        /// <summary>
        /// Creates a memoization delegate from a function receiving one parameter
        /// </summary>
        /// <typeparam name="P1">Type of first parameter</typeparam>
        /// <typeparam name="R">Type of result</typeparam>
        /// <param name="toMemoize">Function to memoize results of</param>
        /// <param name="memoizationContainerProvider">provider of a memoization storage</param>
        /// <returns>A memoizing proxy delegate for<paramref name="toMemoize"/></returns>
        Func<P1, R> From<P1, R>(Func<P1, R> toMemoize, IMemoizationContainerStrategy memoizationContainerProvider);
    }
}