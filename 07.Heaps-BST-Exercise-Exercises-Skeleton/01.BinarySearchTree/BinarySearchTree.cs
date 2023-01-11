﻿namespace _02.BinarySearchTree
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable
    {
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        private Node root;

        private BinarySearchTree(Node node)
        {
            this.PreOrderCopy(node);
        }

        public BinarySearchTree()
        {
        }

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
        }

        public bool Contains(T element)
        {
            Node current = this.FindElement(element);

            return current != null;
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.root, action);
        }

        public IBinarySearchTree<T> Search(T element)
        {
            Node current = this.FindElement(element);

            return new BinarySearchTree<T>(current);
        }

        public void Delete(T element)
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.Delete(this.root, element);
        }

        private Node Delete(Node node, T element)
        {
            //Base Case
            if (node == null)
            {
                return node;
            }

            //If the node were one is bigger than the node we're looking for we recursively call the function by keeping the link between the nodes
            if (node.Value.CompareTo(element) > 0)
            {
                node.Left = this.Delete(node.Left, element);
            }
            //in this case we do the same but return the recursive call from its right child
            else if (node.Value.CompareTo(element) < 0)
            {
                node.Right = this.Delete(node.Right, element);
            }
            //in this case we have found the element
            else
            {
                //case: 1 no children we just delete the node
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }
                //case: 2 we check if the node we're deleting has one child(either left or right)
                else if (node.Left != null && node.Right == null)
                {
                    return node.Left;
                }
                else if (node.Left == null && node.Right != null)
                {
                    return node.Right;
                }
                //Case: 3 both children are present
                else
                {
                    Node leftMax = this.FindLeftSuccessor(node.Left);

                    node.Left = this.Delete(node.Left, leftMax.Value);

                    leftMax.Left = node.Left;
                    leftMax.Right = node.Right;

                    node = leftMax;
                }
            }

            return node;
        }

        private Node FindLeftSuccessor(Node node)
        {
            if (node.Right != null)
            {
                return this.FindLeftSuccessor(node.Right);
            }
            else if (node.Left != null)
            {
                return this.FindLeftSuccessor(node.Left);
            }
            else return node;
        }

        public void DeleteMax()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            var biggestElement = this.root.Value;

            Action<T> CompareElement = comparer =>
            {
                if (biggestElement.CompareTo(comparer) < 0)
                {
                    biggestElement = comparer;
                }
            };

            this.EachInOrder(this.root.Right ,CompareElement);
            this.Delete(biggestElement);
        }

        public void DeleteMin()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            var smallestElement = this.root.Value;

            Action<T> CompareElement = comparer =>
            {
                if (smallestElement.CompareTo(comparer) > 0)
                {
                    smallestElement = comparer;
                }
            };

            this.EachInOrder(this.root.Left ,CompareElement);
            this.Delete(smallestElement);
        }

        public int Count()
        {
            return this.Count(this.root);
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + this.Count(node.Left) + this.Count(node.Right);
        }
        public int Rank(T element)
        {
            return this.Count() - this.Rank(this.root, element);
        }

        private int Rank(Node node, T element)
        {
            if (node == null)
            {
                return 0;
            }

            if (node.Value.CompareTo(element) > 0)
            {
                return 1 + this.Count(node.Right) + this.Rank(node.Left, element);
            }
            else if (node.Value.CompareTo(element) < 0)
            {
                return this.Rank(node.Right, element);
            }
            else
            {
                return this.Count(node);
            }
        }

        public T Select(int rank)
        {
            throw new NotImplementedException();
        }

        public T Ceiling(T element)
        {
            throw new NotImplementedException();
        }

        public T Floor(T element)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            bool hasSwitched = this.CheckRange(ref startRange,ref endRange);

            var result = new List<T>();
            Action<T> addToResult = x =>
            {
                if (x.CompareTo(startRange) >= 0 && x.CompareTo(endRange) <= 0)
                {
                    result.Add(x);
                }
            };
            this.EachInOrder(addToResult);

            if (hasSwitched == false)
                return result;
            else
                return result.OrderByDescending(x => x);
        }

        private bool CheckRange(ref T startRange,ref T endRange)
        {
            if (startRange.CompareTo(endRange) > 0)
            {
                var temp = startRange;
                startRange = endRange;
                endRange = temp;
                return true;
            }
            return false;
        }

        private Node FindElement(T element)
        {
            Node current = this.root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else if (current.Value.CompareTo(element) < 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                node = new Node(element);
            }
            else if (element.CompareTo(node.Value) < 0)
            {
                node.Left = this.Insert(element, node.Left);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.Right = this.Insert(element, node.Right);
            }

            return node;
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }
    }
}
