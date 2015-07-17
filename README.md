#Purple Kingdom Games Core Libs

Contains a number of libraries for everyday problems. All libraries can be found under the 'PurpleKingdomGames.Core'
namespace, and subsequent namespaces under that

## PurpleKingdomGames.Core.Collection

### BinaryHeap
Provides a minimum [binary heap](https://en.wikipedia.org/wiki/Binary_heap) generic implementation with 3
public methods. A binary heap can be constructed with any Type that extends
[IComparable](https://msdn.microsoft.com/en-us/library/system.icomparable%28v=vs.110%29.aspx)

 * Add: Add an item to the heap
 * Remove: Returns the item with the lowest value off of the heap
 * Sort: Re-sort the heap
