using System.Collections;
using System.Collections.Generic;

namespace Elementary.Memoization
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MemoizedEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> memoizedEnumerable;
        private IEnumerator<T> memoizedEnumerator;
        private readonly List<T> memoizedItems = new List<T>();
        private bool memoizedEnumeratorEmpty = false;

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        public MemoizedEnumerable(IEnumerable<T> source)
        {
            this.memoizedEnumerable = source;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            // the enumeration starts with the already collected items.

            foreach (var item in this.memoizedItems)
                yield return item;

            // if no (more) items are left in the cache, the interbnal data source is
            // ask for additional items.

            if (this.memoizedEnumeratorEmpty)
                yield break; // the internal data source is already read completely
            
            if (this.memoizedEnumerator == null)
                this.memoizedEnumerator = this.memoizedEnumerable.GetEnumerator();

            while (this.memoizedEnumerator.MoveNext())
            {
                // call current only once. Current itself could be expensive
                // even if it si nad style to implement expensive code as a property
                var tmp = this.memoizedEnumerator.Current;

                // add the item to the cache and return it
                this.memoizedItems.Add(tmp);
                yield return tmp;
            }

            // both cached items and inner data source have no items left
            // dispose the internal enumerator to free its resources

            this.memoizedEnumeratorEmpty = true;
            this.memoizedEnumerator.Dispose();
            this.memoizedEnumerator = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}