using System;
using System.Collections.Generic;

namespace PurpleKingdomGames.Core.Collections
{
    /// <summary>
    /// A binary heap implementation for any types that extend IComparable
    /// </summary>
    /// <typeparam name="T">Any type that extends IComparable</typeparam>
    public class BinaryHeap<T> where T : IComparable
    {
        /// <summary>
        /// The number of items on the heap
        /// </summary>
        public int Count
        {
            get
            {
                return items.Length;
            }
        }

        private T[] items = new T[0];

        /// <summary>
        /// Add a single item to the heap
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            Array.Resize<T>(ref items, items.Length + 1);
            items[items.Length - 1] = item;

            Sort(items.Length - 1);
        }

        /// <summary>
        /// Get the lowest value from the top of the heap
        /// </summary>
        /// <returns></returns>
        public T Remove()
        {
            T returnItem = items[0];
            items[0] = items[items.Length - 1];

            Array.Resize<T>(ref items, items.Length - 1);

            SortAfterRemoval();

            return returnItem;
        }

        /// <summary>
        /// Resort the heap from the specified index. Used when an items value changes
        /// </summary>
        /// <param name="index">The index of the item that changed value</param>
        /// <exception cref="ArgumentOutOfRangeException">If the index is out of range</exception>
        public void Sort(int index)
        {
            if (index >= items.Length) {
                throw new ArgumentOutOfRangeException("Index must be less than " + items.Length + " (got " + index + ")");
            }

            if (index < 0) {
                throw new ArgumentOutOfRangeException("Index must be greater than zero (got " + index + ")");
            }

            int m = index + 1;
            while (m != 1) {
                int compM = (int) Math.Floor(m / 2f);
                if (items[m - 1].CompareTo(items[compM - 1]) <= 0) {
                    T temp = items[compM - 1];
                    items[compM - 1] = items[m - 1];
                    items[m - 1] = temp;

                    m = compM;
                } else {
                    break;
                }
            }
        }

        /// <summary>
        /// Gets the index of the specified item in the heap
        /// </summary>
        /// <param name="item">The item to search for</param>
        /// <returns>Returns zero or more on success. -1 on failure</returns>
        public int IndexOf(T item)
        {
            for (int i = 0; i < items.Length; i++) {
                if (item.Equals(items[i])) {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Resort the heap
        /// </summary>
        private void SortAfterRemoval()
        {
            int i = 1;
            while (true) {
                int u = i;
                int compU = 2 * u;
                if (compU < items.Length) {
                    if (items[u - 1].CompareTo(items[compU - 1]) >= 0) {
                        i = compU;
                    }

                    if (items[i - 1].CompareTo(items[compU]) >= 0) {
                        i = compU + 1;
                    }
                } else if (compU - 1 < items.Length) {
                    if (items[u - 1].CompareTo(items[compU - 1]) >= 0) {
                        i = compU;
                    }
                }

                if (u == i) {
                    break;
                }

                T temp = items[u - 1];
                items[u - 1] = items[i - 1];
                items[i - 1] = temp;
            }
        }
    }
}
