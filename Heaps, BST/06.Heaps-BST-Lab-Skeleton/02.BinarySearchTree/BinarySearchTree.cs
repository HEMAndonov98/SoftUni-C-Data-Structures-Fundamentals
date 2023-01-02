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
            //=> this.Contains(element, this.root) != null;
            => this.Contains2(element, this.root); //(this method I implemented uses recursion to find the node)//

        private Node Contains(T element, Node node)
        {
            while (node != null)
            {
                if (element.CompareTo(node.Value) < 0)
                {
                    node = node.LeftChild;
                }
                else if (element.CompareTo(node.Value) > 0)
                {
                    node = node.RightChild;
                }
                else
                {
                    break;
                }
            }

            return node;
        }
        private bool Contains2(T element, Node node)
        {
            if (node == null)
            {
                return false;
            }

            if (element.CompareTo(node.Value) < 0)
            {
               return this.Contains2(element, node.LeftChild);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                return this.Contains2(element, node.RightChild);
            }

            return true;
        }

        public void EachInOrder(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                node = new Node(element);
            }
            else if (element.CompareTo(node.Value) < 0)
            {
               node.LeftChild = this.Insert(element, node.LeftChild);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.RightChild = this.Insert(element, node.RightChild);
            }

            return node;
        }

        public IBinarySearchTree<T> Search(T element)
        {
            throw new NotImplementedException();
        }
    }
}
