using System.Collections;
using System.Text;

namespace DataStructures
{
    public sealed class SingleLinkedList <T> : IEnumerable<T>
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

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public void Add (T data)
        {
            var node = new Node<T>(data);

            if (head == null)
            {
                head = node;
            }
            else
            {
                tail!.Next = node;
            }

            tail = node;
            count++;
        }

        public void AddFirst (T data)
        {
            var node = new Node<T>(data)
            {
                Next = head
            };

            head = node;

            tail ??= head;

            count++;
        }

        public bool Contains(T data)
        {
            Node<T>? current = head;

            while (current != null)
            {
                if (Equals(current.Data, data))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public bool Remove (T data)
        {
            if (head == null)
                return false;

            Node<T>? current = head;
            Node<T>? previous = null;

            while(current != null)
            {
                if(Equals(current.Data, data))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;

                        if (current.Next == null)
                        {
                            tail = previous;
                        }
                    } 
                    else
                    {
                        head = head?.Next;

                        if (head == null)
                        {
                            tail = null;
                        }
                    }

                    count--;
                    return true;
                }

                previous = current;
                current = current.Next;
            }
            return false;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T>? current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
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

            while (current != null)
            {
                string data = current.Data?.ToString() ?? "NULL";
                string next = current.Next?.Data?.ToString() ?? "NULL";

                stringBuilder.AppendLine($"{data.PadRight(25)} > {next.PadRight(25)}");
                current = current.Next;
            }

            return stringBuilder.ToString();
        }
    }
}
