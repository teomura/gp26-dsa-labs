using System;
using System.Collections.Generic;

namespace DSA.Lab04B
{
    /// <summary>
    /// Priority queue implemented with a binary min-heap.
    ///
    /// Invariant:
    /// each parent's Priority is <= the Priority of each of its children.
    ///
    /// The heap is NOT fully sorted.
    /// </summary>
    public sealed class BinaryHeapDispatchQueue<T>
    {
        private readonly List<PriorityEntry<T>> _heap = new List<PriorityEntry<T>>();

        public int Count => _heap.Count;

        public void Enqueue(T item, int priority)
        {
            _heap.Add(new PriorityEntry<T>(item, priority));
            int currentIndex = _heap.Count - 1;
            SiftUp(currentIndex);
            // TODO:
            // 1. Append the new entry at the end.
            // 2. Restore the heap invariant upward.
        }

        public PriorityEntry<T> PeekNext()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException("The dispatch queue is empty.");
            }

            return _heap[0];
        }

        public PriorityEntry<T> DequeueNext()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException("The dispatch queue is empty.");
            }
            PriorityEntry<T> root = _heap[0];
            Swap(_heap.Count - 1, 0);
            _heap.RemoveAt(_heap.Count - 1);
            SiftDown(0);

            return root;
            // TODO:
            // 1. Reject an empty queue.
            // 2. Remember the root: that is the minimum.
            // 3. Move the final heap entry to the root and remove the final slot.
            // 4. If anything remains, restore the invariant downward.
            // 5. Return the old root.
        }

        private void SiftUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = ParentIndex(index);
                if (_heap[parentIndex].Priority > _heap[index].Priority)
                {
                    Swap(parentIndex, index);
                    index = parentIndex;
                }
                else
                {
                    break;
                }
            }

            // TODO: compare the entry with its parent and swap while needed.
        }

        private void SiftDown(int index)
        {
            while (index < _heap.Count)
            {
                int leftIndex = LeftChildIndex(index);
                int rightIndex = RightChildIndex(index);
                if (leftIndex < _heap.Count && rightIndex < _heap.Count)
                {
                    int minPriorityIndex = 0;
                    if (_heap[leftIndex].Priority > _heap[rightIndex].Priority)
                    {
                        minPriorityIndex = rightIndex;
                    }
                    else
                    {
                        minPriorityIndex = leftIndex;
                    }
                    if (_heap[index].Priority > _heap[minPriorityIndex].Priority)
                    {
                        Swap(index, minPriorityIndex);
                        index = minPriorityIndex;
                    }
                }
                else if (leftIndex < _heap.Count)
                {
                    if (_heap[index].Priority > _heap[leftIndex].Priority)
                    {
                        Swap(index, leftIndex);
                    }
                    break;
                }
                else
                {
                    break;
                }
            }
            // TODO:
            // Compare the current entry with its children.
            // If a child should come first, swap with the smaller-priority child.
        }

        private static int ParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        private static int LeftChildIndex(int index)
        {
            return 2 * index + 1;
        }

        private static int RightChildIndex(int index)
        {
            return 2 * index + 2;
        }

        private void Swap(int a, int b)
        {
            PriorityEntry<T> temp = _heap[a];
            _heap[a] = _heap[b];
            _heap[b] = temp;
        }
    }
}