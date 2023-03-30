using System.Collections;
using System.Text;

namespace DataStructures
{
    public sealed class BinaryTree<T> : IEnumerable<T> where T : IComparable
    {
        private enum Side
        {
            Root,
            Left,
            Right
        }

        private class Node<TData> where TData : IComparable
        {
            public Node<TData>? Left { get; set; }
            public Node<TData>? Right { get; set; }
            public Node<TData>? Parent { get; set; }

            public TData Data { get; set; }
            public Side Side { get; set; } = Side.Root;

            public bool IsLeaf => Left == null && Right == null;

            public Node(TData data)
            {
                Data = data;
            }
        }

        private Node<T>? tree;

        public void Add(T data)
        {
            Node<T> node = new(data);

            if (tree == null)
            {
                tree = node;
                node.Side = Side.Root;
            }
            else
            {
                Insert(node, tree);
            }
        }

        private void Insert(Node<T> node, Node<T> parent)
        {
            if (node.Data.CompareTo(parent.Data) < 0)
            {
                if (parent.Left == null)
                {
                    parent.Left = node;
                    node.Parent = parent;
                    node.Side = Side.Left;
                    return;
                }
                else
                {
                    Insert(node, parent.Left);
                }
            }
            else
            {
                if (parent.Right == null)
                {
                    parent.Right = node;
                    node.Parent = parent;
                    node.Side = Side.Right;
                    return;
                }
                else
                {
                    Insert(node, parent.Right);
                }
            }
        }

        public bool Remove(T data)
        {
            Node<T>? node = SearchNode(data);

            if (node == null)
                return false;

            var nodeSide = node.Side;

            if (node.IsLeaf)
            {
                if (node.Parent != null)
                {
                    if (nodeSide == Side.Left)
                    {
                        node.Parent.Left = null;
                    }
                    else
                    {
                        node.Parent.Right = null;
                    }
                    node.Parent = null;
                }
                else
                if (node == tree)
                {
                    tree = null;
                }
                return true;
            }
            else
            if (node.Left == null && node.Right != null)
            {
                if (node.Parent != null)
                {
                    if (nodeSide == Side.Left)
                    {
                        node.Parent.Left = node.Right;
                        node.Right.Side = Side.Left;
                    }
                    else
                    {
                        node.Parent.Right = node.Right;
                    }
                    node.Right.Parent = node.Parent;
                }
                else
                {
                    var replacementRightRight = node.Right.Right;
                    var replacementRightLeft = node.Right.Left;

                    node.Data = node.Right.Data;
                    node.Right = replacementRightRight;
                    node.Left = replacementRightLeft;

                    if (replacementRightRight != null)
                        replacementRightRight.Parent = node;

                    if (replacementRightLeft != null)
                        replacementRightLeft.Parent = node;
                }
                return true;
            }
            else
            if (node.Left != null && node.Right == null)
            {
                if (node.Parent != null)
                {
                    if (nodeSide == Side.Left)
                    {
                        node.Parent.Left = node.Left;
                    }
                    else
                    {
                        node.Parent.Right = node.Left;
                        node.Left.Side = Side.Right;
                    }

                    node.Left.Parent = node.Parent;
                } 
                else
                {
                    var replacementLeftRight = node.Left.Right;
                    var replacementLeftLeft = node.Left.Left;

                    node.Data = node.Left.Data;
                    node.Right = replacementLeftRight;
                    node.Left = replacementLeftLeft;

                    if (replacementLeftRight != null)
                        replacementLeftRight.Parent = node;

                    if (replacementLeftLeft != null)
                        replacementLeftLeft.Parent = node;
                }
                return true;
            }
            else
            {
                if (node.Left != null && node.Right != null)
                {
                    if (node.Parent != null)
                    {
                        switch (nodeSide)
                        {
                            case Side.Left:
                                node.Parent.Left = node.Right;
                                node.Right.Parent = node.Parent;
                                node.Right.Side = Side.Left;

                                Insert(node.Left, node.Right);
                                return true;
                            case Side.Right:
                                node.Parent.Right = node.Right;
                                node.Right.Parent = node.Parent;

                                Insert(node.Left, node.Right);
                                return true;
                        }
                        throw new Exception("Node has Parent, but it Side is Root");
                    }
                    else
                    {
                        var replacementLeft = node.Left;
                        var replacementRightLeft = node.Right.Left;
                        var replacementRightRight = node.Right.Right;

                        node.Data = node.Right.Data;
                        node.Right = replacementRightRight;
                        node.Left = replacementRightLeft;

                        if (replacementRightLeft != null)
                            replacementRightLeft.Parent = node;

                        if (replacementRightRight != null)
                            replacementRightRight.Parent = node;

                        Insert(replacementLeft, node);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Contains (T data)
        {
            return SearchNode(data) != null;
        }

        private Node<T>? SearchNode(T data)
        {
            return SearchNode(tree, data);
        }

        private Node<T>? SearchNode(Node<T>? node, T data)
        {
            if (node == null) return null;
            return data.CompareTo(node.Data) switch
            {
                1 => SearchNode(node.Right, data),
                -1 => SearchNode(node.Left, data),
                0 => node,
                _ => null,
            };
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            if (tree != null)
            {
                foreach (var node in EnumerateNodes(tree))
                {
                    yield return node.Data;
                }
            }
        }

        private IEnumerable<Node<T>> EnumerateNodes(Node<T>? node)
        {
            if (node != null)
            {
                yield return node;

                if (node.Left != null)
                {
                    foreach (var childNode in EnumerateNodes(node.Left))
                    {
                        yield return childNode;
                    }
                }

                if (node.Right != null)
                {
                    foreach (var childNode in EnumerateNodes(node.Right))
                    {
                        yield return childNode;
                    }
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
            BuildTree(stringBuilder, tree, Side.Root);

            return stringBuilder.ToString();

            static void BuildTree(StringBuilder stringBuilder, Node<T>? node, Side side, char indentChar = ' ', int indentSize = 0)
            {
                if (node != null)
                {
                    if (side == Side.Root)
                    {
                        stringBuilder.AppendLine($"[{side.ToString()}] - {node.Data}");
                    }
                    else
                    {
                        string indent = new(indentChar, indentSize);
                        stringBuilder.AppendLine($"{indent}[{side.ToString()}] - {node.Data}");
                    }
                    indentSize++;
                    BuildTree(stringBuilder, node.Left, Side.Left, indentChar, indentSize++);
                    BuildTree(stringBuilder, node.Right, Side.Right, indentChar, indentSize++);
                }
            }
        }
    }
}
