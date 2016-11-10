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
    public class MemoizedEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> enumerable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public MemoizedEnumerable(IEnumerable<T> source)
        {
            this.enumerable = source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new MemoizedEnumerator<T>(this.enumerable.GetEnumerator());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
