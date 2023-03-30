using System.Collections;
using System.Text;

namespace DataStructures
{
    public sealed class Stack<T> : IEnumerable<T>
    {
        private class Node <TData>
        {
            public TData Data { get; set; }
            public Node<TData>? Previous { get; set; }

            public Node(TData data)
            {
                Data = data;
            }
        }

        private Node<T>? head;
        private int count;

        public int Count => count;
        public bool IsEmpty => count == 0;

        public void Push (T data)
        {
            Node<T> node = new(data)
            {
                Previous = head
            };
            head = node;
            count++;
        }

        public T Pop ()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Stack is empty!");
            }

            count--;
            var data = head.Data;
            head = head.Previous;

            return data;
        }

        public T Peek()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Stack is empty!");
            }

            var data = head.Data;
            return data;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T>? current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Previous;
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
                string previous = current.Previous?.Data?.ToString() ?? "NULL";

                stringBuilder.AppendLine($"{data.PadRight(25)} > {previous.PadRight(25)}");
                current = current.Previous;
            }

            return stringBuilder.ToString();
        }
    }
}
