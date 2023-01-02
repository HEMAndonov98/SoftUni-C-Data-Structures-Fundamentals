namespace _02.BinarySearchTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        private BinarySearchTree(Node node)
        {
            this.PreOrederCopy(node);
        }

        private void PreOrederCopy(Node node)
        {
            this.Insert(node.Value);

            if (node.LeftChild != null)
            {
                this.PreOrederCopy(node.LeftChild);
            }

            if (node.RightChild != null)
            {
                this.PreOrederCopy(node.RightChild);
            }
        }

        public bool Contains(T element)
            //=> this.Contains(element, this.root) != null (intuitive implementation by lecturer);
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
            //this.EachInOrder(action, this.root) - recursive implementation;
            foreach (var item in this.InOrder(this.root))
            {
                action.Invoke(item);
            }
        }

        private IEnumerable<T> InOrder(Node root)
        {
            var result = new List<T>();

            if (root.LeftChild != null)
            {
                foreach (var item in this.InOrder(root.LeftChild))
                {
                    result.Add(item);
                }
            }

            result.Add(root.Value);

            if (root.RightChild != null)
            {
                foreach (var item in this.InOrder(root.RightChild))
                {
                    result.Add(item);
                }
            }

            return result;
        } //Intuitive implementaiton

        private void EachInOrder(Action<T> action, Node node)
        {
            if (node.LeftChild != null)
            {
                this.EachInOrder(action, node.LeftChild);
            }

            action.Invoke(node.Value);

            if (node.RightChild != null)
            {
                this.EachInOrder(action, node.RightChild);
            }
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
            var node = this.Contains(element, this.root);

            if (node == null)
            {
                return null;
            }

            return new BinarySearchTree<T>(node);
        }
    }
}
