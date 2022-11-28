namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private class Node
        {
            public Node(T value, Node next)
            {
                this.Value = value;
                this.Next = next;
            }

            public T Value { get; set; }
            public Node Next { get; set; }
        }

        private Node head;

        public int Count { get; private set; }

        public void Enqueue(T item)
        {
            var newNode = new Node(item, null);
            if (this.Count == 0)
            {
                this.head = newNode;
            }
            else
            {
                var currentNode = this.head;
                while (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                currentNode.Next = newNode;
            }
            this.Count++;
        }

        public T Dequeue()
        {
            this.CheckCollectionSize();

            var oldHead = this.head;
            var newHead = this.head.Next;

            this.head = newHead;
            this.Count--;
            return oldHead.Value;
        }

        public T Peek()
        {
            this.CheckCollectionSize();
            return this.head.Value;
        }

        public bool Contains(T item)
        {
            var currentNode = this.head;

            while (currentNode != null)
            {
                if (currentNode.Value.Equals(item))
                    return true;

                currentNode = currentNode.Next;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void CheckCollectionSize()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}