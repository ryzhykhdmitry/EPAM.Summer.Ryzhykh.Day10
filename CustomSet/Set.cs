using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSet
{
    public class CustomSet<T> : IEnumerable<T> where T : class
    {
        #region Fields
        private Hashtable set;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CustomSet<T> class that is empty.
        /// </summary>
        public CustomSet()
        {
            set = new Hashtable();
        }

        /// <summary>
        /// Initializes a new instance of the CustomSet<T> class that 
        /// contains elements copied from the specified collection.
        /// </summary>
        /// <param name="other">Some collection.</param>
        public CustomSet(IEnumerable<T> other)
        {
            set = new Hashtable();
            if (!ReferenceEquals(other, null))
            {
                foreach (var item in other.Distinct())
                {
                    set.Add(item.GetHashCode(), item);
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Return count of the elements. 
        /// </summary>
        public int Count
        {
            get
            {
                return set.Count;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Adds the specified element to a set.
        /// </summary>
        /// <param name="item">Element.</param>
        /// <returns></returns>
        public bool Add(T item)
        {
            if (ReferenceEquals(item, null)) throw new ArgumentNullException();
            bool ret = set.Contains(item.GetHashCode());
            if (!ret) set.Add(item.GetHashCode(), item);
            return ret;
        }

        /// <summary>
        /// Removes all elements from a CustomSet<T> object.
        /// </summary>
        public void Clear()
        {
            set.Clear();
        }

        /// <summary>
        /// Determines whether a CustomSet<T> object contains the specified element.
        /// </summary>
        /// <param name="item">Element.</param>
        /// <returns>True if the CustomSet<T> object contains the specified element; 
        /// otherwise, false.</returns>
        public bool Contains(T item)
        {
            return set.Contains(item.GetHashCode());
        }

        /// <summary>
        /// Copies the elements of a CustomSet<T> object to an array, 
        /// starting at the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination 
        /// of the elements copied from the CustomSet<T> object.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (ReferenceEquals(array, null)) throw new ArgumentNullException();
            set.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes all elements in the specified collection from the current CustomSet<T> object.
        /// </summary>
        /// <param name="other">Specified collection.</param>
        public void ExceptWith(CustomSet<T> other)
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException();
            foreach (DictionaryEntry item in other.set)
            {
                if (set.ContainsKey(item.Key))
                    set.Remove(item.Key);
            }
        }

        /// <summary>
        /// Modifies the current CustomSet<T> object to contain only elements that
        /// are present in that object and in the specified collection.
        /// </summary>
        /// <param name="other">Specified collection.</param>
        public void IntersectWith(CustomSet<T> other)
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException();
            Hashtable part1 = GetHashTable();
            foreach (DictionaryEntry item in set)
            {
                if (!other.set.Contains(item.Key)) part1.Remove(item.Key);
            }
            set = part1;
        }

        /// <summary>
        /// Modifies the current CustomSet<T> object to contain only elements that 
        /// are present either in that object or in the specified collection, but not both.
        /// </summary>
        /// <param name="other">Specified collection.</param>
        public void SymmetricExceptWith(CustomSet<T> other)
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException();
            Hashtable first = GetHashTable();
            Hashtable second = other.GetHashTable();
            foreach (DictionaryEntry item in set)
            {
                if (other.set.Contains(item.Key)) first.Remove(item.Key);
            }
            foreach (DictionaryEntry item in other.set)
            {
                if (set.Contains(item.Key)) second.Remove(item.Key);
            }
            set = first;
            foreach (DictionaryEntry item in second)
            {
                set.Add(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Modifies the current CustomSet<T> object to contain all elements that 
        /// are present in itself, the specified collection, or both.
        /// </summary>
        /// <param name="other">Specified collection.</param>
        public void UnionWith(CustomSet<T> other)
        {
            if (ReferenceEquals(other, null)) throw new ArgumentNullException();
            foreach (DictionaryEntry item in other.set)
            {
                if (!set.ContainsKey(item.Key))
                    set.Add(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Removes the specified element from a HashSet<T> object.
        /// </summary>
        /// <param name="item">Specified element.</param>
        /// <returns></returns>
        public void Remove(T item)
        {
            if (ReferenceEquals(item, null)) throw new ArgumentNullException();
            set.Remove(item.GetHashCode());
        }
        #endregion

        #region Enumerator
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator<T> object that can be used to iterate through
        /// the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var e in set.Values)
            {
                yield return (dynamic)e;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator<T> object that can be used to iterate through
        /// the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Equals(T other)
        {
            return base.Equals(other);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes a new instance of the Hashtable class that contains set.
        /// </summary>
        /// <returns></returns>
        private Hashtable GetHashTable()
        {
            return new Hashtable(set);
        } 
        #endregion
    }
}

