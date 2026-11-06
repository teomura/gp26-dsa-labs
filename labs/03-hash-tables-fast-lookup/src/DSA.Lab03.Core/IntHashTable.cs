using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace DSA.Lab03
{
    public sealed class IntHashTable<TValue>
    {
        private readonly List<Entry<TValue>>[] _buckets;
        private int _count;

        public IntHashTable(int bucketCount)
        {
            if (bucketCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bucketCount));
            }

            _buckets = new List<Entry<TValue>>[bucketCount];
            for (int i = 0; i < _buckets.Length; ++i)
            {
                _buckets[i] = new List<Entry<TValue>>();
            }
        }

        public int Count => _count;
        public int BucketCount => _buckets.Length;

        public void Set(int key, TValue value)
        {
            int index = GetBucketIndex(key);// search only the correct bucket
            List<Entry<TValue>> list = _buckets[index];
            for (int i = 0; i < list.Count; i++)
            {
                if (key == list[i].Key)
                {
                    list[i] = new Entry<TValue>(key, value);//
                    return;                                 // replace
                }
            }
            list.Add(new Entry<TValue>(key, value));// append and increment
            _count++;
            // TODO: search only the correct bucket, replace or append.
        }

        public bool TryGetValue(int key, out TValue value)
        {
            value = default!;
            int index = GetBucketIndex(key);
            List<Entry<TValue>> list = _buckets[index];
            for (int i = 0; i < list.Count; i++)
            {
                if (key == list[i].Key)
                {
                    value = list[i].Value;
                    return true;
                }
            }
            return false;

            // TODO: search only the correct bucket.
        }

        public bool ContainsKey(int key)
        {
            int index = GetBucketIndex(key);
            List<Entry<TValue>> list = _buckets[index];
            for (int i = 0; i < list.Count; i++)
            {
                if (key == list[i].Key)
                {
                    return true;
                }
            }
            return false;
            // TODO: reuse the lookup behavior if useful.
        }

        private int GetBucketIndex(int key)
        {
            if (key < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(key), "This learning implementation uses non-negative integer keys.");
            }

            return key % _buckets.Length;
        }
    }
}