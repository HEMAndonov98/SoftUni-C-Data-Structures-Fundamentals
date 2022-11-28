namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
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

        public void AddFirst(T item)
        {
            if (this.Count == 0)
            {
                var newHead = new Node(item, null);
                this.head = newHead;
            }
            else
            {
                var oldHead = this.head;
                var newHead = new Node(item, oldHead);
                this.head = newHead;
            }
            this.Count++;
        }

        public void AddLast(T item)
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

        public T GetFirst()
        {
            this.CheckCollectionSize();
            return this.head.Value;
        }

        public T GetLast()
        {
            this.CheckCollectionSize();

            var currentNode = this.head;
            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            }
            return currentNode.Value;
        }

        public T RemoveFirst()
        {
            this.CheckCollectionSize();
            var oldHead = this.head;
            var newHead = this.head.Next;

            this.head = newHead;
            this.Count--;
            return oldHead.Value;
        }

        public T RemoveLast()
        {
            this.CheckCollectionSize();
            T removedValue = default;

            if (this.head.Next == null)
            {
                removedValue = this.head.Value;
                this.head = null;
                this.Count--;
                return removedValue;
            }

            var currentNode = this.head;
            //Check if current node is second to last
            while (currentNode.Next.Next != null)
            {
                currentNode = currentNode.Next;
            }
            //Save removed node value
            removedValue = currentNode.Next.Value;
            //Remove last node by setting it to null
            currentNode.Next = null;
            this.Count--;
            //Return saved value
            return removedValue;
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
                throw new InvalidOperationException("Cannot perform this operation on an empty collection");
            }
        }
    }
}