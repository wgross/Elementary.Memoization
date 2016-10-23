namespace Elementary.Memoization
{
    using Elementary.Memoization.ContainerStrategies;
    using Elementary.Memoization.DelegateFactories;
    using System;
    using System.Collections.Generic;

    public sealed class MemoizationBuilder
    {
        private IMemoizationDelegateFactory memoizationDelegateFactory = new MemoizeWithTuplesFactory();

        private IMemoizationContainerStrategy memoizationStorageFactory = new MemoizeInDictionaryStrategy();

        #region Builder allows to select the delegate builder factory

        /// <summary>
        /// Use <see cref="System.Tuple"/> as a key for the mapping of parameters to calculation result.
        /// This is the recommended method for memoization.
        /// </summary>
        /// <returns>The next builder step: selection of a memoization container provider</returns>
        public SelectStorageFactory MapFromParameterTuples()
        {
            return this.MapFrom(new MemoizeWithTuplesFactory());
        }

        /// <summary>
        /// Use Currying to create a tree of memoization delegates, mapping on each level one paremeter
        /// to a delegate mapping the next parameter to a delegate and so on, until the last parameter
        /// maps to the result of the calculation.
        /// This mapping method can be very fast for retrieval of data but slow for building new tree branches.
        /// Use it only after comparison of performance with <see cref="MapFromParameterTuples"/>.
        /// </summary>
        /// <returns>The next builder step: selection of a memoization container provider</returns>
        public SelectStorageFactory MapFromCurriedParameters()
        {
            return this.MapFrom(new MemoizeWithDelegatesFactory());
        }

        private SelectStorageFactory MapFrom(IMemoizationDelegateFactory selectedMemoizationDelegateFactory)
        {
            this.memoizationDelegateFactory = selectedMemoizationDelegateFactory;

            return new SelectStorageFactory(this);
        }

        #endregion Builder allows to select the delegate builder factory

        #region Builder allows to select a storage factory

        public class SelectStorageFactory
        {
            private readonly MemoizationBuilder builder;

            internal SelectStorageFactory(MemoizationBuilder builder)
            {
                this.builder = builder;
            }

            /// <summary>
            /// The references of the result value are stored in strong references (if result type is a reference type)
            /// </summary>
            /// <returns>The final step of the memoization delegate builder</returns>
            public CreateFinalDelegate StoreInDictionaryWithStrongReferences()
            {
                return this.StoreIn(new MemoizeInDictionaryStrategy());
            }

            /// <summary>
            /// Only one (latest) value is memoized.
            /// </summary>
            /// <returns>The final step of the memoization delegate builder</returns>
            public CreateFinalDelegate StoreLatestResultOnlyWithStrongReferences()
            {
                return this.StoreIn(new MemoizeLatestResultOnlyStrategy());
            }

            /// <summary>
            /// The references to the result are stored in an IDictionary implementation provided by the caller.
            /// This allows sharing or reuse of an existing memoization container.
            /// </summary>
            /// <typeparam name="K"></typeparam>
            /// <typeparam name="V"></typeparam>
            /// <param name="memoizationContainer"></param>
            /// <returns>The final step of the memoization delegate builder</returns>
            public CreateFinalDelegate StoreInExternalDictionaryWithStrongReferences<K, V>(IDictionary<K, V> memoizationContainer)
            {
                return this.StoreIn(new MemoizeInExistingDictionaryStrategy(memoizationContainer));
            }

            /// <summary>
            /// Use weak references to store the references of the result.
            /// Use this storage container if Memoization shall not interfere with the garbage collector.
            /// </summary>
            /// <returns>The final step of the memoization delegate builder</returns>
            public CreateFinalDelegate StoreInDictionaryWithWeakReferences()
            {
                return this.StoreIn(new MemoizeInDictionaryWeakStrategy());
            }

            /// <summary>
            /// Use string references and a <see cref="ConcurrentDictionary{TKey, TValue}"/> instance. This provides a threadsafe
            /// memoization container if the memoized delegate is shared between multiple threads.
            /// </summary>
            /// <returns>The final step of the memoization delegate builder</returns>
            public CreateFinalDelegate StoreInConcurrentDictionaryWithStrongReferences()
            {
                return this.StoreIn(new MemoizeStrongReferencesThreadSafeStrategy());
            }

            /// <summary>
            /// Use a custom memoization container provider implementation.
            /// </summary>
            /// <param name="memoizationContainerProvider">Custom memoization provider implementation</param>
            /// <returns>The final step of the memoization delegate builder</returns>
            public CreateFinalDelegate StoreIn(IMemoizationContainerStrategy memoizationContainerProvider)
            {
                if (memoizationContainerProvider == null)
                    throw new ArgumentNullException(nameof(memoizationContainerProvider));

                this.builder.memoizationStorageFactory = memoizationContainerProvider;

                return new CreateFinalDelegate(this.builder);
            }
        }

        #endregion Builder allows to select a storage factory

        #region Build memoization delegate with selected memoization and reference management strategy

        public class CreateFinalDelegate
        {
            private readonly MemoizationBuilder builder;

            internal CreateFinalDelegate(MemoizationBuilder builder)
            {
                this.builder = builder;
            }

            public Func<P1, P2, P3, P4, P5, P6, P7, R> From<P1, P2, P3, P4, P5, P6, P7, R>(Func<P1, P2, P3, P4, P5, P6, P7, R> toMemoize)
            {
                return this.builder.memoizationDelegateFactory.From<P1, P2, P3, P4, P5, P6, P7, R>(toMemoize, this.builder.memoizationStorageFactory);
            }

            public Func<P1, P2, P3, P4, P5, P6, R> From<P1, P2, P3, P4, P5, P6, R>(Func<P1, P2, P3, P4, P5, P6, R> toMemoize)
            {
                return this.builder.memoizationDelegateFactory.From<P1, P2, P3, P4, P5, P6, R>(toMemoize, this.builder.memoizationStorageFactory);
            }

            public Func<P1, P2, P3, P4, P5, R> From<P1, P2, P3, P4, P5, R>(Func<P1, P2, P3, P4, P5, R> toMemoize)
            {
                return this.builder.memoizationDelegateFactory.From<P1, P2, P3, P4, P5, R>(toMemoize, this.builder.memoizationStorageFactory);
            }

            public Func<P1, P2, P3, P4, R> From<P1, P2, P3, P4, R>(Func<P1, P2, P3, P4, R> toMemoize)
            {
                return this.builder.memoizationDelegateFactory.From<P1, P2, P3, P4, R>(toMemoize, this.builder.memoizationStorageFactory);
            }

            public Func<P1, P2, P3, R> From<P1, P2, P3, R>(Func<P1, P2, P3, R> toMemoize)
            {
                return this.builder.memoizationDelegateFactory.From<P1, P2, P3, R>(toMemoize, this.builder.memoizationStorageFactory);
            }

            public Func<P1, P2, R> From<P1, P2, R>(Func<P1, P2, R> toMemoize)
            {
                return this.builder.memoizationDelegateFactory.From<P1, P2, R>(toMemoize, this.builder.memoizationStorageFactory);
            }

            public Func<P1, R> From<P1, R>(Func<P1, R> toMemoize)
            {
                return this.builder.memoizationDelegateFactory.From<P1, R>(toMemoize, this.builder.memoizationStorageFactory);
            }
        }

        #endregion Build memoization delegate with selected memoization and reference management strategy
    }
}