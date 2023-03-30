using System.Collections;
using System.Text;

namespace DataStructures
{
    public class SkipList<T> : IEnumerable<T> where T : IComparable
    {
        private class Node<TData> where TData : IComparable
        {
            public Node<TData>[] Next { get; private set; }
            public TData? Data { get; private set; }

            public Node(int level)
            {
                Next = new Node<TData>[level];
            }

            public Node(TData? data, int level)
            {
                Data = data;
                Next = new Node<TData>[level];
            }
        }

        private static readonly Random rand = new();

        private readonly Node<T> head = new(level: 33);
        private int levels = 1;

        public void Insert(T data)
        {
            int level = 0;
            for (int r = rand.Next(); (r & 1) == 1; r >>= 1)
            {
                level++;

                if (level == levels)
                {
                    levels++; break;
                }
            }

            Node<T> node = new(data, level + 1);
            Node<T> current = head;

            for (int i = levels - 1; i >= 0; i--)
            {
                while (current.Next[i] != null)
                {
                    if (data.CompareTo(current.Next[i].Data) < 0)
                    {
                        break;
                    }
                    current = current.Next[i];
                }

                if (i <= level)
                {
                    node.Next[i] = current.Next[i];
                    current.Next[i] = node;
                }
            }
        }

        public bool Contains(T data)
        {
            Node<T> current = head;
            for (int i = levels - 1; i >= 0; i--)
            {
                while (current.Next[i] != null)
                {
                    if (data.CompareTo(current.Next[i].Data) < 0)
                    {
                        break;
                    }

                    if (data.CompareTo(current.Next[i].Data) == 0)
                    {
                        return true;
                    }

                    current = current.Next[i];
                }
            }
            return false;
        }

        public bool Remove(T data)
        {
            Node<T> current = head;

            bool found = false;
            for (int i = levels - 1; i >= 0; i--)
            {
                while (current.Next[i] != null)
                {
                    if (data.CompareTo(current.Next[i].Data) == 0)
                    {
                        found = true;
                        current.Next[i] = current.Next[i].Next[i];
                        break;
                    }

                    if (data.CompareTo(current.Next[i].Data) < 0)
                    {
                        break;
                    }

                    current = current.Next[i];
                }
            }

            return found;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = head.Next[0];
            while (current != null)
            {
                if (current.Data != null)
                {
                    yield return current.Data;
                }
                current = current.Next[0];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            Node<T> current;
            for (int i = levels - 1; i >= 0; i--)
            {
                current = head.Next[i];
                stringBuilder.Append($"[L{i.ToString()}] : ");

                while (current != null)
                {
                    string? value = current.Data?.ToString();
                    stringBuilder.Append($"[{value}:{current.Next.Length}] > ");

                    current = current.Next[i];
                }

                stringBuilder.Append("[NIL]\n");
            }
            return stringBuilder.ToString();
        }
    }
}
