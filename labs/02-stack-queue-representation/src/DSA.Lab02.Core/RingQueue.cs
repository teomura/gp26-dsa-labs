using System;

namespace DSA.Lab02
{
    public sealed class RingQueue<T>
    {
        private readonly T[] _items;
        private int _head;
        private int _tail;
        private int _count;

        public RingQueue(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _items = new T[capacity];
        }

        public int Count => _count;
        public int Capacity => _items.Length;

        public void Enqueue(T item)
        {
            if (_count == _items.Length)// overflow safety
            {
                // TODO: write at tail, advance tail with wrap-around, update count.
                throw new InvalidOperationException();
            }
            _items[_tail] = item;//write at tail
            _tail = (_tail + 1) % _items.Length;//advance tail with wrap-around
            _count++;//update count
        }

        public T Peek()
        {
            if (_count == 0)// underflow safety
            {
                // TODO: return the item at head without removing it.
                throw new InvalidOperationException();
            }
            return _items[_head];// return the item at head without removing it
        }

        public T Dequeue()
        {
            if (_count == 0)
            {
                // TODO: read/clear at head, advance head with wrap-around, update count.
                throw new InvalidOperationException();
            }
            T item = _items[_head];// read item at the current head
            _items[_head] = default;// clear item at the head
            _head = (_head + 1) % _items.Length;// advance head with weap-around
            _count--;//update count
            return item;
        }
    }
}