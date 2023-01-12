namespace _02.BinarySearchTree
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
            //the internal Rank function returns the number of elements bigger than X
            //so we just subtract the total count and the number of elements bigger than X to get the number of elements smaller than x
            return this.Count() - this.Rank(this.root, element);
        }

        //To find how many elements are smaller than a given element we have to see how many elements are bigger than it
        private int Rank(Node node, T element)
        {
            if (node == null)
            {
                return 0;
            }

            //Case: 1 the node is bigger than the searched element
            if (node.Value.CompareTo(element) > 0)
            {
                //In this case we just sum all the elements to its right(including the current node)
                return 1 + this.Count(node.Right) + this.Rank(node.Left, element);//We call the function recursively to add up all the elements bigger than the probided one
            }
            //Case: 2 the node is smaller than the searched node
            else if (node.Value.CompareTo(element) < 0)
            {
                //Because the node is smaller it is in the count of the smaller elements this is why we just call the function again to see if we're on the right node
                return this.Rank(node.Right, element);
            }
            else
            {
                //Here we have found the element so we just sum all the elements to its right plus this node so we can add them up recursively
                return  1 + this.Count(node.Right);
            }
        }

        public T Select(int rank)
        {
            Node currentNode = this.root;

            if (currentNode == null)
            {
                throw new InvalidOperationException();
            }

            while (currentNode != null)
            {
                if (this.Rank(currentNode.Value) < rank)
                {
                    currentNode = currentNode.Right;
                }
                else if (this.Rank(currentNode.Value) > rank)
                {
                    currentNode = currentNode.Left;
                }
                else
                {
                    break;
                }
            }

            if (currentNode == null)
            {
                throw new InvalidOperationException();
            }

            return currentNode.Value;

        }

        public T Ceiling(T element)
        {
            Node node = this.FindElement(element);

            if (node == null)
            {
                throw new InvalidOperationException();
            }

            if (node.Right == null)
            {
                throw new InvalidOperationException();
            }

            while (node != null)
            {
                if (node.Left != null)
                {
                    node = node.Left;
                }
                else
                {
                    break;
                }
            }

            return node.Value;
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
