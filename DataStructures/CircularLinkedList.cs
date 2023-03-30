using System.Collections;
using System.Text;

namespace DataStructures
{
    public sealed class CircularLinkedList<T> : IEnumerable<T>
    {
        private class Node<TData>
        {
            public TData Data { get; set; }
            public Node<TData>? Next { get; set; }

            public Node(TData data)
            {
                Data = data;
            }
        }

        private Node<T>? head;
        private Node<T>? tail;
        private int count;

        public int Count => count;
        public bool IsEmpty => count == 0;

        public bool Contains(T data)
        {
            Node<T>? current = head;

            do
            {
                if (current != null)
                {
                    if (Equals(current.Data, data))
                    {
                        return true;
                    }
                    current = current.Next;
                }
            }
            while (current != head);

            return false;
        }

        public void Add(T data)
        {
            Node<T> node = new(data);

            if (head == null)
            {
                head = node;
                tail = node;
                tail.Next = head;
            }
            else
            {
                node.Next = head;
                tail!.Next = node;
                tail = node;
            }
            count++;
        }

        public bool Remove (T data)
        {
            if (head == null)
                return false;

            Node<T>? current = head;
            Node<T>? previous = null;

            do
            {
                if (Equals(current.Data, data))
                {
                    if (previous == null)
                    {
                        if (head.Next != head)
                        {
                            head = head?.Next;
                            tail!.Next = head;
                        }
                        else
                        {
                            head = tail = null;
                        }
                    }
                    else
                    {
                        previous.Next = current.Next;

                        if (current == tail)
                        {
                            tail = previous;
                        }
                    }

                    count--;
                    return true;
                }

                previous = current;
                current = current.Next;
            }
            while (current != null && current != head);

            return false;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T>? current = head;

            do
            {
                if (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
            while (current != head);
        }

        public IEnumerable<T> GetEnumerator(int offset)
        {
            int count = Count;
            int skip = offset;

            Node<T>? current = head;

            while (count > 0)
            {
                if (current != null)
                {
                    if (skip > 0)
                    {
                        skip--;
                    }
                    else
                    {
                        yield return current.Data;
                        count--;
                    }
                    current = current.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            Node<T>? current = head;

            do
            {
                if (current != null)
                {
                    string data = current.Data?.ToString() ?? "NULL";
                    string next = current.Next?.Data?.ToString() ?? "NULL";

                    stringBuilder.AppendLine($"{data.PadRight(25)} > {next.PadRight(25)}");
                    current = current.Next;
                }
            }
            while (current != head);
            return stringBuilder.ToString();
        }
    }
}
