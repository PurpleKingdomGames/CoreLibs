using System;
using System.Collections.Generic;
using NUnit.Framework;
using PurpleKingdomGames.Core.Collection;

namespace PurpleKingdomGames.Test.Core.Collection
{
    [TestFixture]
    class BinaryHeapTest
    {
        /// <summary>
        /// Test that the 'Add' method works and increments the count
        /// </summary>
        [Test]
        public void TestAdd()
        {
            BinaryHeap<int> heap = new BinaryHeap<int>();

            Assert.AreEqual(0, heap.Count);
            
            heap.Add(1);
            Assert.AreEqual(1, heap.Count);

            heap.Add(1);
            Assert.AreEqual(2, heap.Count);

            heap.Add(2);
            Assert.AreEqual(3, heap.Count);
        }

        /// <summary>
        /// Test that the 'Remove' method works correctly
        /// </summary>
        [Test]
        public void TestRemove()
        {
            BinaryHeap<int> heap = new BinaryHeap<int>();
            int[] testData = new int[10] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            List<int> sortedList = new List<int>();
            sortedList.AddRange(testData);

            Random r = new Random();

            int randomIndex = 0;
            while (sortedList.Count > 0) {
                randomIndex = r.Next(0, sortedList.Count);
                heap.Add(testData[randomIndex]);

                sortedList.RemoveAt(randomIndex);
            }

            for (int i = 0; i < testData.Length; i++) {
                Assert.AreEqual(i, heap.Remove());
            }
        }
    }
}
