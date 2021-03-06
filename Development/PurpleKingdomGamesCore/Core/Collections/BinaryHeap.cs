﻿using PurpleKingdomGames.Core.Collections.Internal;
using System;

namespace PurpleKingdomGames.Core.Collections
{
    /// <summary>
    /// A binary heap implementation for any types that extend IComparable
    /// </summary>
    /// <typeparam name="T">Any type that extends IComparable</typeparam>
    public class BinaryHeap<T> where T : IComparable
    {
        /// <summary>
        /// Return an item at the specified index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= items.Length) {
                    throw new IndexOutOfRangeException(
                        "Index cannot be below zero or more than " + (items.Length - 1) + ". Got " + index
                    );
                }

                return items[index].Item1;
            }
        }

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

        private Tuple<T, int>[] items = new Tuple<T, int>[0];

        /// <summary>
        /// Add a single item to the heap
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            AddInternal(item);
        }

        /// <summary>
        /// Get the lowest value from the top of the heap
        /// </summary>
        /// <returns></returns>
        public T Remove()
        {
            int index = items.Length - 1;
            Tuple<T, int> returnItem = items[0];
            items[0] = items[index];

            Array.Resize<Tuple<T, int>>(ref items, index);
            for (int i = 0; i < items.Length; i++) {
                items[i].Item2 = i;
            }

            SortAfterRemoval();

            return returnItem.Item1;
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
                int compM = (int) Math.Floor(m * 0.5);
                int firstIndex = compM - 1;
                int secondIndex = m - 1;

                if (items[secondIndex].Item1.CompareTo(items[firstIndex].Item1) <= 0) {
                    Tuple<T, int> temp = items[firstIndex];
                    items[firstIndex] = items[secondIndex];
                    items[secondIndex] = temp;

                    items[firstIndex].Item2 = firstIndex;
                    items[secondIndex].Item2 = secondIndex;

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
                if (item.Equals(items[i].Item1)) {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Internal method for adding an item. Only real difference is that it
        /// returns and exposes the underlying stored object, rather than not returning at all
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal Tuple<T, int> AddInternal(T item)
        {
            int index = items.Length;
            int hash = item.GetHashCode();
            Array.Resize<Tuple<T, int>>(ref items, index + 1);
            Tuple<T, int> returnItem = items[index] = new Tuple<T, int>(item, index);

            Sort(index);

            return returnItem;
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
                int firstIndex = u - 1;

                if (compU < items.Length) {
                    if (items[firstIndex].Item1.CompareTo(items[compU - 1].Item1) >= 0) {
                        i = compU;
                    }

                    if (items[i - 1].Item1.CompareTo(items[compU].Item1) >= 0) {
                        i = compU + 1;
                    }
                } else if (compU - 1 < items.Length) {
                    if (items[firstIndex].Item1.CompareTo(items[compU - 1].Item1) >= 0) {
                        i = compU;
                    }
                }

                if (u == i) {
                    break;
                }

                int secondIndex = i - 1;
                Tuple<T, int> temp = items[firstIndex];

                items[firstIndex] = items[secondIndex];
                items[secondIndex] = temp;
                items[firstIndex].Item2 = firstIndex;
                items[secondIndex].Item2 = secondIndex;
            }
        }
    }
}