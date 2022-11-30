namespace Problem01.CircularQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class CircularQueue<T> : IAbstractQueue<T>
    {
        public const int InitCapacity = 4;

        private T[] data;
        private int startIndex;
        private int endIndex;

        public CircularQueue()
        {
            this.data = new T[InitCapacity];
            this.startIndex = 0;
            this.endIndex = 0;
        }

        public int Capacity => this.data.Length;
        public int Count { get; private set; }

        public T Dequeue()
        {
            this.CheckIfCollectionEmpty();
            this.TryShrinkArray();

            T removedItem = this.data[this.startIndex];
            this.data[this.startIndex] = default;

            var newStartIndex = (this.startIndex + 1) % this.Capacity;
            this.startIndex = newStartIndex;
            this.Count--;
            return removedItem;
        }

        public void Enqueue(T item)
        {
            this.TryGrowArray();
            this.data[this.endIndex] = item;

            int newEndIndex = (this.endIndex + 1) % this.Capacity;
            this.endIndex = newEndIndex;

            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentDataIndex = this.startIndex;
            while (currentDataIndex != this.endIndex)
            {
                yield return this.data[currentDataIndex];
                currentDataIndex = (currentDataIndex + 1) % this.Capacity;
            }
        }

        public T Peek()
        {
            this.CheckIfCollectionEmpty();
            T itemToPeek = this.data[this.startIndex];
            return itemToPeek;
        }

        public T[] ToArray()
        {
            var returnArray = new T[this.Count];
            var currentDataIndex = this.startIndex;
            for (int i = 0; i < returnArray.Length; i++)
            {
                returnArray[i] = this.data[currentDataIndex];
                currentDataIndex = (currentDataIndex + 1) % this.Capacity;
            }

            return returnArray;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void TryShrinkArray()
        {
            if (this.Count == this.Capacity / 2
                && this.Capacity > InitCapacity)
            {
                T[] newData = new T[this.Capacity / 2];
                //instead of copying the array and trying to figure out a mathematical way around it just reaorder it so that we start with a fresh array
                var currentDataIndex = this.startIndex;
                for (int i = 0; i < this.Count; i++)
                {
                    newData[i] = this.data[currentDataIndex];
                    currentDataIndex = (currentDataIndex + 1) % this.Capacity;
                }

                this.data = newData;
                this.startIndex = 0;
                this.endIndex = this.Capacity;
            }
        }
        private void TryGrowArray()
        {
            if (this.Count == this.Capacity)
            {
                T[] newData = new T[this.Capacity * 2];
                Array.Copy(this.data, newData, this.Count);
                this.data = newData;
                this.endIndex = this.Count;
            }
        }
        private void CheckIfCollectionEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }

}
