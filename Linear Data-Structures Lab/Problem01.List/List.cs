namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] items;

        public List()
            : this(DEFAULT_CAPACITY) {
        }

        public List(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();
            this.items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(index);
                return this.items[index];
            }
            set
            {
                this.ValidateIndex(index);
                this.items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            this.GrowIfNessecary();

            this.items[this.Count] = item;
            this.Count++;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < this.Count; i++)
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
            for (int i = 0; i < this.Count; i++)
            {
                if (this.items[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);
            this.GrowIfNessecary();
            this.ShiftRight(this.Count, index);
            this.items[index] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            var itemToRemoveIndex = this.IndexOf(item);

            if (itemToRemoveIndex == -1)
            {
                return false;
            }

            this.RemoveAt(itemToRemoveIndex);
            return true;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);
            this.Count--;
            this.ShiftLeft(index, this.Count);
            this.items[this.Count] = default;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
          =>  this.GetEnumerator();

        private void GrowIfNessecary()
        {
            if (this.Count >= this.items.Length)
            {
                this.items = this.Grow(this.items);
            }
        }

        private T[] Grow(T[] items)
        {
            var newArray = new T[this.items.Length * 2];
            Array.Copy(this.items, newArray, this.Count);
            return newArray;
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private void ShiftLeft(int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                this.items[i] = this.items[i + 1];
            }
        }

        private void ShiftRight(int startIndex, int endIndex)
        {
            for (int i = startIndex; i > endIndex; i--)
            {
                this.items[i] = this.items[i - 1];
            }
        }
    }
}