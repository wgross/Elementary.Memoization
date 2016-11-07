namespace Elementary.Memoization.DelegateFactories
{
    using System;

    /// <summary>
    /// Provides factory methods for mememoizing delegates.
    /// Memoization is built up by recursivly mememoizing any parameter until a curriyed version of the original delegates is completed.
    /// The curryied delegate is wrapped with an additional lambda providing a call signature expected by C# programmers:
    /// n Parameters at one delegates instead 1 paramet at a delegate, returning a delegate receiving n-1 parameters.
    /// Building is more expensive as with memoization based on Tuples, but the lookup is fast as hell!
    /// </summary>
    internal sealed class MemoizeWithDelegatesFactory : MemoizationFactoryBase, IMemoizationDelegateFactory
    {
        #region Curryied memoization results to delegates by using partial application: The function returns a function receiving one parameter less

        private Func<P1, Func<P2, Func<P3, Func<P4, Func<P5, Func<P6, Func<P7, R>>>>>>> Curryied<P1, P2, P3, P4, P5, P6, P7, R>(Func<P1, P2, P3, P4, P5, P6, P7, R> toMemoize, IMemoizationContainerStrategy useStrategy = null)
        {
            return this.From<P1, Func<P2, Func<P3, Func<P4, Func<P5, Func<P6, Func<P7, R>>>>>>>((p1) => Curryied<P2, P3, P4, P5, P6, P7, R>((p2, p3, p4, p5, p6, p7) => toMemoize(p1, p2, p3, p4, p5, p6, p7), useStrategy), useStrategy);
        }

        private Func<P1, Func<P2, Func<P3, Func<P4, Func<P5, Func<P6, R>>>>>> Curryied<P1, P2, P3, P4, P5, P6, R>(Func<P1, P2, P3, P4, P5, P6, R> toMemoize, IMemoizationContainerStrategy useStrategy = null)
        {
            return this.From<P1, Func<P2, Func<P3, Func<P4, Func<P5, Func<P6, R>>>>>>((p1) => Curryied<P2, P3, P4, P5, P6, R>((p2, p3, p4, p5, p6) => toMemoize(p1, p2, p3, p4, p5, p6), useStrategy), useStrategy);
        }

        private Func<P1, Func<P2, Func<P3, Func<P4, Func<P5, R>>>>> Curryied<P1, P2, P3, P4, P5, R>(Func<P1, P2, P3, P4, P5, R> toMemoize, IMemoizationContainerStrategy useStrategy = null)
        {
            return this.From<P1, Func<P2, Func<P3, Func<P4, Func<P5, R>>>>>((p1) => Curryied<P2, P3, P4, P5, R>((p2, p3, p4, p5) => toMemoize(p1, p2, p3, p4, p5), useStrategy), useStrategy);
        }

        private Func<P1, Func<P2, Func<P3, Func<P4, R>>>> Curryied<P1, P2, P3, P4, R>(Func<P1, P2, P3, P4, R> toMemoize, IMemoizationContainerStrategy useStrategy = null)
        {
            return this.From<P1, Func<P2, Func<P3, Func<P4, R>>>>((p1) => Curryied<P2, P3, P4, R>((p2, p3, p4) => toMemoize(p1, p2, p3, p4), useStrategy), useStrategy);
        }

        private Func<P1, Func<P2, Func<P3, R>>> Curryied<P1, P2, P3, R>(Func<P1, P2, P3, R> toMemoize, IMemoizationContainerStrategy useStrategy = null)
        {
            return this.From<P1, Func<P2, Func<P3, R>>>((p1) => Curryied<P2, P3, R>((p2, p3) => toMemoize(p1, p2, p3), useStrategy), useStrategy);
        }

        private Func<P1, Func<P2, R>> Curryied<P1, P2, R>(Func<P1, P2, R> toMemoize, IMemoizationContainerStrategy useStrategy = null)
        {
            return this.From<P1, Func<P2, R>>((p1) => this.From<P2, R>(p2 => toMemoize(p1, p2), useStrategy), useStrategy);
        }

        #endregion Curryied memoization results to delegates by using partial application: The function returns a function receiving one parameter less

        #region IMemoizationFactory Members

        public Func<P1, P2, P3, P4, P5, P6, P7, R> From<P1, P2, P3, P4, P5, P6, P7, R>(Func<P1, P2, P3, P4, P5, P6, P7, R> toMemoize, IMemoizationContainerStrategy useStrategy = null)
        {
            var tmp = Curryied<P1, P2, P3, P4, P5, P6, P7, R>(toMemoize, useStrategy);
            return (p1, p2, p3, p4, p5, p6, p7) => tmp(p1)(p2)(p3)(p4)(p5)(p6)(p7);
        }

        public Func<P1, P2, P3, P4, P5, P6, R> From<P1, P2, P3, P4, P5, P6, R>(Func<P1, P2, P3, P4, P5, P6, R> toMemoize, IMemoizationContainerStrategy useStrategy = null)
        {
            var tmp = Curryied<P1, P2, P3, P4, P5, P6, R>(toMemoize, useStrategy);
            return (p1, p2, p3, p4, p5, p6) => tmp(p1)(p2)(p3)(p4)(p5)(p6);
        }

        public Func<P1, P2, P3, P4, P5, R> From<P1, P2, P3, P4, P5, R>(Func<P1, P2, P3, P4, P5, R> toMemoize, IMemoizationContainerStrategy useStrategy = null)
        {
            var tmp = Curryied<P1, P2, P3, P4, P5, R>(toMemoize, useStrategy);
            return (p1, p2, p3, p4, p5) => tmp(p1)(p2)(p3)(p4)(p5);
        }

        public Func<P1, P2, P3, P4, R> From<P1, P2, P3, P4, R>(Func<P1, P2, P3, P4, R> toMemoize, IMemoizationContainerStrategy useStrategy = null)
        {
            var tmp = Curryied<P1, P2, P3, P4, R>(toMemoize, useStrategy);
            return (p1, p2, p3, p4) => tmp(p1)(p2)(p3)(p4);
        }

        public Func<P1, P2, P3, R> From<P1, P2, P3, R>(Func<P1, P2, P3, R> toMemoize, IMemoizationContainerStrategy useStrategy = null)
        {
            var tmp = Curryied<P1, P2, P3, R>(toMemoize, useStrategy);
            return (p1, p2, p3) => tmp(p1)(p2)(p3);
        }

        public Func<P1, P2, R> From<P1, P2, R>(Func<P1, P2, R> toMemoize, IMemoizationContainerStrategy useStrategy = null)
        {
            var tmp = Curryied<P1, P2, R>(toMemoize, useStrategy);
            return (p1, p2) => tmp(p1)(p2);
        }

        #endregion IMemoizationFactory Members
    }
}