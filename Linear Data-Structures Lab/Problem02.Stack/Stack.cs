namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private class Node
        {
            public Node()
            {
            }

            public Node(T element, Node next)
            {
                this.Element = element;
                this.Next = next;
            }

            public T Element { get; set; }
            public Node Next { get; set; }
        }

        private Node top;

        public int Count { get; private set; }

        public void Push(T item)
        {
            if (this.Count == 0)
            {
                this.top = new Node(item, null);
            }
            else
            {
            var oldTop = this.top;
            this.top = new Node(item, oldTop);
            }
            this.Count++;
        }

        public T Pop()
        {
            this.CheckIfStackEmpty();
            var nodeToRemove = this.top;
            var newTop = this.top.Next;
            this.top.Next = null;
            this.top = newTop;
            this.Count--;
            return nodeToRemove.Element;
        }

        public T Peek()
        {
            this.CheckIfStackEmpty();
            return this.top.Element;
        }

        public bool Contains(T item)
        {
            var node = this.top;
            while (node != null)
            {
                if (node.Element.Equals(item))
                {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.top;
            while (current != null)
            {
                yield return current.Element;
                current = current.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void CheckIfStackEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}