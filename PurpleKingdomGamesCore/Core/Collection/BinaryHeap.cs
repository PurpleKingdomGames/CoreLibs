using System;
using System.Collections.Generic;

namespace PurpleKingdomGames.Core.Collection
{
    public class BinaryHeap<T> where T : IComparable
    {
        public int Count
        {
            get
            {
                return items.Count;
            }
        }

        private List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
            int m = items.Count - 1;
            while (m != 0) {
                if (items[m].CompareTo(items[m / 2]) <= 0) {
                    T temp = items[m / 2];
                    items[m / 2] = items[m];
                    items[m] = temp;

                    m = m / 2;
                } else {
                    break;
                }
            }
        }

        public T Remove()
        {
            T returnItem = items[0];
            items.RemoveAt(0);

            Sort();

            return returnItem;
        }

        public void Sort()
        {
            int i = 1;
            while (true) {
                int u = i;
                if (((2 * u) + 1) <= items.Count) {
                    if (items[u].CompareTo(items[2 * u]) >= 0) {
                        i = 2 * u;
                    }

                    if (items[i].CompareTo(items[(2 * u) + 1]) >= 0) {
                        i = (2 * u) + 1;
                    }
                } else if ((2 * u) <= items.Count) {
                    if (items[u].CompareTo(items[2 * u]) >= 0) {
                        i = 2 * u;
                    }
                }

                if (u == i) {
                    break;
                }

                T temp = items[u];
                items[u] = items[i];
                items[i] = temp;
            }
        }
    }
}
