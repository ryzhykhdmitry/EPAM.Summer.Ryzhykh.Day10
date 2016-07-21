using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomGenericQueue
{
    public class CustomQueue<T> : IEnumerable<T>
    {
        #region Fields
        private LinkedList<T> queue; 
        #endregion

        #region Properties
        /// <summary>
        /// Return count of elements.
        /// </summary>
        public int Count { get { return queue.Count; } } 
        #endregion

        #region Constructors
        public CustomQueue()
        {
            queue = new LinkedList<T>();
        }

        /// <summary>
        /// Create CustomQueue by collection.
        /// </summary>
        /// <param name="collection">Collection of elements.</param>
        public CustomQueue(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException();
            queue = new LinkedList<T>(collection);
        }

        /// <summary>
        /// Create CustomQueue by params.
        /// </summary>
        /// <param name="arr">Array of elements.</param>
        public CustomQueue(params T[] arr) : this((IEnumerable<T>)arr)
        { }
        #endregion

        #region Methods
        /// <summary>
        /// Add element to the queue.
        /// </summary>
        /// <param name="value">Element.</param>
        public void Enqueue(T value)
        {
            if (value == null) throw new ArgumentNullException();
            queue.AddLast(new LinkedListNode<T>(value));
        }

        /// <summary>
        /// Remove first element from queue.
        /// </summary>
        /// <returns>First element.</returns>
        public T Dequeue()
        {
            if (ReferenceEquals(null, queue.First))
                throw new ArgumentNullException();
            T result = queue.First.Value;
            queue.RemoveFirst();
            return result;
        }

        /// <summary>
        /// Show the first element of the queue w/o deletion. 
        /// </summary>
        /// <returns>First ewlement.</returns>
        public T Peek()
        {
            if (ReferenceEquals(null, queue.First))
                throw new ArgumentNullException();
            return queue.First.Value;
        }
        #endregion

        #region Enumerators
        public IEnumerator<T> GetEnumerator()
        {
            return new CustomQueueIterator(queue);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } 
        #endregion

        #region Iterator
        private class CustomQueueIterator : IEnumerator<T>
        {
            #region Fields
            private LinkedList<T> queue;
            private int position = -1;
            #endregion

            #region Constructor
            public CustomQueueIterator(LinkedList<T> source)
            {
                queue = new LinkedList<T>(source);
            }
            #endregion

            #region Implementation of IEnumerator<T> methods
            /// <summary>
            /// Go to the next position
            /// </summary>
            /// <returns>False, if collection is finished</returns>
            public bool MoveNext()
            {
                if (position == queue.Count + 1)
                {
                    Reset();
                    return false;
                }
                position++;
                return true;
            }

            /// <summary>
            /// Set index to the pre-begin
            /// </summary>
            public void Reset()
            {
                position = -1;
            }

            /// <summary>
            /// Return current element
            /// </summary>
            object IEnumerator.Current
            {
                get { return queue.ToArray()[position]; }
            }           

            public void Dispose()
            {
            }
            #endregion

            #region Public property
            /// <summary>
            /// Return current element
            /// </summary>
            public T Current
            {
                get { return queue.ToArray()[position]; }
            }
            #endregion
        }
        #endregion

    }
}
