using System.Collections;
using System.Text;

namespace DataStructures
{
    public sealed class Deque<T> : IEnumerable<T>
    {
        private class Node<TData>
        {
            public TData Data { get; set; }
            public Node<TData>? Previous { get; set; }
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

        public void EnqueueFirst(T data)
        {
            Node<T> node = new(data);

            if (head != null)
            {
                head.Previous = node;
            }

            node.Next = head;

            head = node;

            tail ??= head;

            count++;
        }

        public T DequeueFirst()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Nothing to dequeue. Deque is empty");
            }

            var data = head.Data;

            head = head.Next;

            if (head == null)
            {
                tail = null;
            }
            else
            {
                head.Previous = null;
            }

            count--;

            return data;
        }

        public void EnqueueLast(T data)
        {
            Node<T> node = new(data);

            if (head == null)
            {
                head = node;
            }
            else
            {
                tail!.Next = node;
                node.Previous = tail;
            }

            tail = node;
            count++;
        }

        public T DequeueLast()
        {
            if (tail == null)
            {
                throw new InvalidOperationException("Nothing to dequeue. Deque is empty");
            }

            var data = tail.Data;

            tail = tail.Previous;

            if (tail == null)
            {
                head = null;
            }
            else
            {
                tail.Next = null;
            }

            count--;

            return data;
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
                string prev = current.Previous?.Data?.ToString() ?? "NULL";
                string next = current.Next?.Data?.ToString() ?? "NULL";

                stringBuilder.AppendLine($"{prev.PadRight(25)} > {data.PadRight(25)} > {next.PadRight(25)}");
                current = current.Next;
            }

            return stringBuilder.ToString();
        }
    }
}
