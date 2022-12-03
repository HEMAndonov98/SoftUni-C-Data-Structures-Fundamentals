namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public Node(T value, Node next, Node previous)
            {
                this.Value = value;
                this.Next = next;
                this.Previous = previous;
            }

            public Node Next { get; set; }
            public Node Previous { get; set; }
            public T Value { get; set; }
        }

        private Node head;
        private Node tail;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            if (this.Count == 0)
            {
                this.AddToEmptyCollection(item);
            }
            else
            {
                var oldHead = this.head;
                var newHead = new Node(item, oldHead, null);

                oldHead.Previous = newHead;
                this.head = newHead;
            }
            this.Count++;
        }

        public void AddLast(T item)
        {
            if (this.Count == 0)
            {
                this.AddToEmptyCollection(item);
            }
            else
            {
                var oldTail = this.tail;
                var newTail = new Node(item, null, oldTail);

                oldTail.Next = newTail;
                this.tail = newTail;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.CheckIfListEmpty();
            return this.head.Value;
        }

        public T GetLast()
        {
            this.CheckIfListEmpty();
            return this.tail.Value;
        }

        public T RemoveFirst()
        {
            this.CheckIfListEmpty();

            var oldHead = this.head;

            if (this.Count == 1)
            {
                this.ClearCollection();
            }
            else
            {
                var newHead = this.head.Next;
                newHead.Previous = null;

                this.head = newHead;
            }

            this.Count--;
            return oldHead.Value;
        }

        public T RemoveLast()
        {
            this.CheckIfListEmpty();

            var oldTail = this.tail;

            if (this.Count == 1)
            {
                this.ClearCollection();
            }
            else
            {
                var newTail = this.tail.Previous;
                newTail.Next = null;

                this.tail = newTail;
            }

            this.Count--;
            return oldTail.Value;
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

        private void CheckIfListEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }

        private void AddToEmptyCollection(T item)
        {
            var newNode = new Node(item, null, null);
            this.head = newNode;
            this.tail = newNode;
        }

        private void ClearCollection()
        {
            this.head = null;
            this.tail = null;
        }
    }
}