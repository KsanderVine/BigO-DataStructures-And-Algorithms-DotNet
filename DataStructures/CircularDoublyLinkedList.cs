using System.Collections;

namespace DataStructures
{
    public sealed class CircularDoublyLinkedList<T> : IEnumerable<T>
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
        private int count;

        public int Count => count;
        public bool IsEmpty => count == 0;

        public void Add (T data)
        {
            Node<T> node = new(data);

            if (head == null)
            {
                head = node;
                head.Next = node;
                head.Previous = node;
            }
            else
            {
                node.Previous = head.Previous;
                node.Next = head;
                head.Previous!.Next = node;
                head.Previous = node;
            }
            count++;
        }

        public bool Remove (T data)
        {
            if (head == null)
                return false;

            Node<T>? current = head;
            Node<T>? removedNode = null;

            do
            {
                if (Equals(current.Data, data))
                {
                    removedNode = current;
                    break;
                }
                current = current.Next;
            }
            while (current != null && current != head);

            if(removedNode != null)
            {
                if (count == 1)
                {
                    head.Next = null;
                    head.Previous = null;
                    head = null;
                } 
                else
                {
                    removedNode.Previous!.Next = removedNode.Next;
                    removedNode.Next!.Previous = removedNode.Previous;

                    if (removedNode == head)
                    {
                        head = head.Next;
                    }
                }

                count--;
                return true;
            }

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
    }
}
