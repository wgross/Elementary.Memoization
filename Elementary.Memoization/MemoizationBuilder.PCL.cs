//namespace Elementary.Memoization
//{
//    using Elementary.Memoization.ContainerStrategies;

//    public sealed partial class MemoizationBuilder
//    {
//        #region Builder allows to select a storage factory

//        public partial class SelectStorageFactory
//        {
//            /// <summary>
//            /// Use string references and a ConcurrentDictionary instance. This provides a threadsafe
//            /// memoization container if the memoized delegate is shared between multiple threads.
//            /// </summary>
//            /// <returns>The final step of the memoization delegate builder</returns>
//            public CreateFinalDelegate StoreInConcurrentDictionaryWithStrongReferences()
//            {
//                return this.StoreIn(new MemoizeStrongReferencesThreadSafeStrategy());
//            }
//        }

//        #endregion Builder allows to select a storage factory
//    }
//}