namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this.items = new T[capacity];
        }

        private int endIndex => this.Count - 1;

        public T this[int index]
        {
            get
            {
                this.CheckIfValidIndex(index);
                return this.items[this.endIndex - index];
            }
            set
            {
                this.CheckIfValidIndex(index);
                this.items[this.endIndex - index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            this.TryToGrow();

            this.Count++;
            this.items[this.endIndex] = item;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i <= this.endIndex; i++)
            {
                if (this.items[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = this.endIndex; i >= 0; i--)
            {
                if (this.items[i].Equals(item))
                {
                    //This returns the index that the client should see(as if weve added the items in reverse order)
                    return this.endIndex - i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            this.CheckIfValidIndex(index);

            //Here by subtracting the endIndex we get our actual index of adding
            // 0 | 1 | 2 looks like to the client 2 | 1 | 0
            //so insert at index 0 means we want to insert at index 2 actually
            var actualIndex = this.endIndex - index;

            this.ShiftRight(actualIndex);
            this.items[actualIndex] = item;

            //We have to swap the items
            var swapedItem = this.items[actualIndex];
            this.items[actualIndex] = this.items[actualIndex + 1];
            this.items[actualIndex + 1] = swapedItem;

            this.Count++;
        }

        public bool Remove(T item)
        {
            this.CheckIfColletionEmpty();
            var itemIndex = this.IndexOf(item);

            if (itemIndex != -1)
            {
                this.ShitfLeft(this.endIndex - itemIndex);
                this.items[this.endIndex] = default;
                this.Count--;
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            this.CheckIfValidIndex(index);
            this.ShitfLeft(this.endIndex - index);
            this.items[endIndex] = default;
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.endIndex; i >= 0; i--)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void Grow()
        {
            var newArray = new T[this.items.Length * 2];

            Array.Copy(this.items, newArray, this.Count);
            this.items = newArray;
        }

        private void TryToGrow()
        {
            if (this.Count == this.items.Length)
            {
                this.Grow();
            }
        }

        private void CheckIfValidIndex(int index)
        {
            if (index < 0 | index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private void CheckIfColletionEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }

        private void ShiftRight(int insertionIndex)
        {
            for (int i = this.Count; i > insertionIndex; i--)
            {
                this.items[i] = this.items[i - 1];
            }
        }

        private void ShitfLeft(int index)
        {
            for (int i = index; i < this.endIndex; i++)
            {
                this.items[i] = this.items[i + 1];
            }
        }
    }
}