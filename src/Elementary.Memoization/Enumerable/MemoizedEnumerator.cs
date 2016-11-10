using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elementary.Memoization
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MemoizedEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> memoizedSource;

        internal MemoizedEnumerator(IEnumerator<T> source)
        {
            this.memoizedSource = source;
        }

        /// <summary>
        /// 
        /// </summary>
        public T Current
        {
            get
            {
                return this.memoizedSource.Current;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.memoizedSource.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            return this.memoizedSource.MoveNext();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            this.memoizedSource.Reset();
        }
    }
}
