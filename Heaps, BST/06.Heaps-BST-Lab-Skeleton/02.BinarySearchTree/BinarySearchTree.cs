namespace _02.BinarySearchTree
{
    using System;

    public class BinarySearchTree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private Node root;

        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.LeftChild = this.RightChild = null;
            }

            public T Value { get; private set; }
            public Node LeftChild { get; set; }
            public Node RightChild { get; set; }
        }

        public BinarySearchTree()
        {
        }

        public bool Contains(T element)
        {
            throw new NotImplementedException();
        }

        public void EachInOrder(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
        }

        private Node Insert(T element, Node root)
        {
            if (root == null)
            {
                root = new Node(element);
            }
            else if (element.CompareTo(root.Value) < 0)
            {
               root.LeftChild = this.Insert(element, root.LeftChild);
            }
            else if (element.CompareTo(root.Value) > 0)
            {
                root.RightChild = this.Insert(element, root.RightChild);
            }
        }

        public IBinarySearchTree<T> Search(T element)
        {
            throw new NotImplementedException();
        }
    }
}
