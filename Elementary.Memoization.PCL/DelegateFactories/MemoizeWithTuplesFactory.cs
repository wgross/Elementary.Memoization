namespace Elementary.Memoization.DelegateFactories
{
    using System;

    /// <summary>
    /// Provides factory methods for memoizing delegates.
    /// Memoization is built with Framework Tuples stored in a Dictionary. This is the common way to implement memoization in C#/.Net.
    /// Building the memoization structures is faster as delegate based memoization but the lookup is slower. Most probably because of
    /// the calculation of the hash code of the tuples.
    /// </summary>
    internal sealed class MemoizeWithTuplesFactory : MemoizationFactoryBase, IMemoizationDelegateFactory
    {
        #region IMemoizationFactory Members

        public Func<P1, P2, P3, P4, P5, P6, P7, R> From<P1, P2, P3, P4, P5, P6, P7, R>(Func<P1, P2, P3, P4, P5, P6, P7, R> toMemoize, IMemoizationContainerStrategy useStrategy)
        {
            var memoization = useStrategy.CreateNew<Tuple<P1, P2, P3, P4, P5, P6, P7>, R>(t => toMemoize(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6, t.Item7));
            return (p1, p2, p3, p4, p5, p6, p7) => memoization(Tuple.Create(p1, p2, p3, p4, p5, p6, p7));
        }

        public Func<P1, P2, P3, P4, P5, P6, R> From<P1, P2, P3, P4, P5, P6, R>(Func<P1, P2, P3, P4, P5, P6, R> toMemoize, IMemoizationContainerStrategy useStrategy)
        {
            var memoization = useStrategy.CreateNew<Tuple<P1, P2, P3, P4, P5, P6>, R>(t => toMemoize(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6));
            return (p1, p2, p3, p4, p5, p6) => memoization(Tuple.Create(p1, p2, p3, p4, p5, p6));
        }

        public Func<P1, P2, P3, P4, P5, R> From<P1, P2, P3, P4, P5, R>(Func<P1, P2, P3, P4, P5, R> toMemoize, IMemoizationContainerStrategy useStrategy)
        {
            var memoization = useStrategy.CreateNew<Tuple<P1, P2, P3, P4, P5>, R>(t => toMemoize(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5));
            return (p1, p2, p3, p4, p5) => memoization(Tuple.Create(p1, p2, p3, p4, p5));
        }

        public Func<P1, P2, P3, P4, R> From<P1, P2, P3, P4, R>(Func<P1, P2, P3, P4, R> toMemoize, IMemoizationContainerStrategy useStrategy)
        {
            var memoization = useStrategy.CreateNew<Tuple<P1, P2, P3, P4>, R>(t => toMemoize(t.Item1, t.Item2, t.Item3, t.Item4));
            return (p1, p2, p3, p4) => memoization(Tuple.Create(p1, p2, p3, p4));
        }

        public Func<P1, P2, P3, R> From<P1, P2, P3, R>(Func<P1, P2, P3, R> toMemoize, IMemoizationContainerStrategy useStrategy)
        {
            var memoization = useStrategy.CreateNew<Tuple<P1, P2, P3>, R>(t => toMemoize(t.Item1, t.Item2, t.Item3));
            return (p1, p2, p3) => memoization(Tuple.Create(p1, p2, p3));
        }

        public Func<P1, P2, R> From<P1, P2, R>(Func<P1, P2, R> toMemoize, IMemoizationContainerStrategy useStrategy)
        {
            var memoization = useStrategy.CreateNew<Tuple<P1, P2>, R>(t => toMemoize(t.Item1, t.Item2));
            return (p1, p2) => memoization(Tuple.Create(p1, p2));
        }

        #endregion IMemoizationFactory Members
    }
}