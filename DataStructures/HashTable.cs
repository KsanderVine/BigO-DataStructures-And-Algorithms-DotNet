using System;
using System.Collections;
using System.Text;
using System.Xml.Linq;

namespace DataStructures
{
    public sealed class HashTable<T> : IEnumerable<T>
    {
        private class Node<TData>
        {
            public Node<TData>? Next { get; set; }
            public string Key { get; set; }
            public TData Value { get; set; }

            public Node(string key, TData value)
            {
                Key = key;
                Value = value;
            }
        }

        private Node<T>?[] buckets;
        private int count;

        public HashTable(uint size = 10)
        {
            if (size == 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            buckets = new Node<T>[size];
        }

        public bool ContainsKey (string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            int bucketNumber = HashTable<T>.GetBucketNumber(key, buckets.Length);

            Node<T>? bucketNode = buckets[bucketNumber];

            if (bucketNode == null)
            {
                return false;
            }

            var (_, found) = HashTable<T>.GetNodeByKey(key, bucketNode);

            return found != null;
        }

        public void Add(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            Node<T> node = new(key, value);

            int bucketNumber = GetBucketNumber(key, buckets.Length);

            Node<T>? bucketNode = buckets[bucketNumber];

            if (bucketNode == null)
            {
                buckets[bucketNumber] = node;
            }
            else
            {
                while (bucketNode.Next != null)
                {
                    if (Equals(bucketNode.Key, key))
                    {
                        throw new ArgumentException("Adding duplicate");
                    }

                    bucketNode = bucketNode.Next;
                }
                bucketNode.Next = node;
            }

            count++;
        }

        public T Get(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            int bucketNumber = HashTable<T>.GetBucketNumber(key, buckets.Length);

            Node<T>? bucketNode = buckets[bucketNumber];

            if (bucketNode == null)
            {
                throw new ArgumentOutOfRangeException(nameof(key));
            }

            var (_, found) = HashTable<T>.GetNodeByKey(key, bucketNode);

            if (found == null)
            {
                throw new NullReferenceException();
            }

            return found.Value;
        }

        public bool Remove (string key)
        {
            int bucketNumber = HashTable<T>.GetBucketNumber(key, buckets.Length);

            Node<T>? bucketNode = buckets[bucketNumber];

            if (bucketNode == null)
            {
                return false;
            }

            var (previous, found) = HashTable<T>.GetNodeByKey(key, bucketNode);

            if (found == null) return false;

            if (previous == null)
            {
                buckets[bucketNumber] = found?.Next;
            }
            else
            {
                previous.Next = found?.Next;
            }

            count--;
            return true;
        }

        private static (Node<T>?, Node<T>?) GetNodeByKey(object key, Node<T> node)
        {
            Node<T>? previous = null;
            Node<T>? current = node;

            while (current != null)
            {
                if (Equals(current.Key, key))
                {
                    return (previous, current);
                }

                previous = current;
                current = current.Next;
            }

            return (null, null);
        }

        private static int GetBucketNumber(object key, int hashsize)
        {
            uint hashcode = HashTable<T>.GetHash(key);
            return (int)(hashcode % (uint)hashsize);
        }

        private static uint GetHash(object key)
        {
            uint hashcode = (uint)key.GetHashCode() & 0x7FFFFFFF;
            return hashcode;
        }

        public override string ToString()
        {
            int elementsLimit = 50;
            StringBuilder stringBuilder = new();

            foreach (var node in buckets.Select((x, i) => (i, x)))
            {
                int elements = 0;
                Node<T>? current = node.x;
                while (current != null)
                {
                    elements++;
                    current = current.Next;
                }

                if (elements >= elementsLimit)
                {
                    stringBuilder.AppendLine($"[B{node.i}][{new string('#', elementsLimit - 2)}+{(elements - (elementsLimit - 2))}]");
                }
                else
                {
                    stringBuilder.AppendLine($"[B{node.i}][{new string('#', elements)}]");
                }
            }

            return stringBuilder.ToString();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (var node in buckets.Select((x, i) => (i, x)))
            {
                Node<T>? current = node.x;
                while (current != null)
                {
                    yield return current.Value;
                    current = current.Next;
                }
            }
        }

        public IEnumerable<(string,T)> GetKeysWithValues()
        {
            foreach (var node in buckets.Select((x, i) => (i, x)))
            {
                Node<T>? current = node.x;
                while (current != null)
                {
                    yield return (current.Key, current.Value);
                    current = current.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
    }
}
