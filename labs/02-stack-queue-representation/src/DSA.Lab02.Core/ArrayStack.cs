using System;

namespace DSA.Lab02
{
    public sealed class ArrayStack<T>
    {
        private readonly T[] _items;
        private int _count;

        public ArrayStack(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _items = new T[capacity];
        }

        public int Count => _count;
        public int Capacity => _items.Length;

        public void Push(T item)
        {
            if (_count == _items.Length)
            {
                throw new InvalidOperationException();
            }
            // TODO: place the item at the logical top and update the state.
            _items[_count] = item;// place the item at the logical top
            _count++;//  update state
        }

        public T Peek()
        {
            if (_count == 0)
            {
                // TODO: return the logical top without removing it.
                throw new InvalidOperationException();
            }
            return _items[_count - 1];
        }

        public T Pop()
        { // TODO: remove and return the logical top.
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            _count--;

            T item = _items[_count];
            _items[_count] = default!;//remove item

            return item;//return item to the top
        }
    }
}