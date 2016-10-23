namespace Elementary.Memoization.DelegateFactories
{
    using System;

    public abstract class MemoizationFactoryBase
    {
        public Func<P, R> From<P, R>(Func<P, R> toMemoize, IMemoizationContainerStrategy useStrategy)
        {
            return useStrategy.CreateNew<P, R>(toMemoize);
        }
    }
}