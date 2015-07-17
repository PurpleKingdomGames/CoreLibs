using System;
using System.Collections.Generic;

namespace PurpleKingdomGames.Core.Collection
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
                return items.Count;
            }
        }

        private List<T> items = new List<T>();

        /// <summary>
        /// Add a single item to the heap
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            items.Add(item);

            Sort(items.Count - 1);
        }

        /// <summary>
        /// Get the lowest value from the top of the heap
        /// </summary>
        /// <returns></returns>
        public T Remove()
        {
            T returnItem = items[0];
            items[0] = items[items.Count - 1];

            items.RemoveAt(items.Count - 1);

            SortAfterRemoval();

            return returnItem;
        }


        /// <summary>
        /// Resort the heap from the specified index. Used when an items value changes
        /// </summary>
        /// <param name="index">The index of the item that changed value</param>
        public void Sort(int index)
        {
            if (index >= items.Count) {
                throw new ArgumentOutOfRangeException("Index must be less than " + items.Count + " (got " + index + ")");
            }
            
            // Move the item to the end of the heap if we're not already
            if (index < (items.Count - 1)) {
                T tmp = items[index];
                items.RemoveAt(index);
                items.Add(tmp);
            }

            int m = items.Count;
            while (m != 1) {
                int compM = (int)Math.Floor(m / 2f);
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
        /// Resort the heap
        /// </summary>
        private void SortAfterRemoval()
        {
            int i = 1;
            while (true) {
                int u = i;
                int compU = 2 * u;
                if (compU < items.Count) {
                    if (items[u - 1].CompareTo(items[compU - 1]) >= 0) {
                        i = compU;
                    }

                    if (items[i - 1].CompareTo(items[compU]) >= 0) {
                        i = compU + 1;
                    }
                } else if (compU - 1 < items.Count) {
                    if (items[u - 1].CompareTo(items[compU - 1]) >= 0) {
                        i = compU;
                    }
                }

                if (u == i) {
                    break;
                }

                T temp = items[u-1];
                items[u-1] = items[i-1];
                items[i-1] = temp;
            }
        }
    }
}
