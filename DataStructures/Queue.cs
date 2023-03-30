using System.Collections;
using System.Text;

namespace DataStructures
{
    public sealed class Queue<T> : IEnumerable<T>
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

        public void Enqueue(T data)
        {
            Node<T> node = new(data);

            if(head == null)
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

        public T Dequeue()
        {
            if (head == null)
            {
                throw new InvalidOperationException("Nothing to dequeue. Queue is empty");
            }

            count--;
            var data = head.Data;

            head = head.Next;

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
                string next = current.Next?.Data?.ToString() ?? "NULL";

                stringBuilder.AppendLine($"{data.PadRight(25)} > {next.PadRight(25)}");
                current = current.Next;
            }

            return stringBuilder.ToString();
        }
    }
}
