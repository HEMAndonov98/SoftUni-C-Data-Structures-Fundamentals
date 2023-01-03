namespace _03.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T> where T : IComparable<T>
    {
        public const int RootNodeIndex = 0;

        private List<T> elements;

        public MaxHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => elements.Count;

        public void Add(T element)
        {
            this.elements.Add(element);
            this.HeapifyUp(this.elements.Count - 1);
        }

        private void HeapifyUp(int index)
        {
            Func<int, int> getParentIndex = i => (i - 1) / 2;

            int parentIndex = getParentIndex(index);

            while (index > 0 && this.IsGreater(index, parentIndex))
            {
                this.Swap(index, parentIndex);

                index = parentIndex;
                parentIndex = getParentIndex(index);
            }
        }

        private void Swap(int index, int parentIndex)
        {
            var temp = this.elements[index];

            this.elements[index] = this.elements[parentIndex];
            this.elements[parentIndex] = temp;
        }

        private bool IsGreater(int index, int parentIndex)
            => this.elements[index].CompareTo(this.elements[parentIndex]) > 0;

        public T ExtractMax()
        {
            if (this.Size == 0)
                throw new InvalidOperationException();

            var extracted = this.Peek();

            this.Swap(RootNodeIndex, this.Size - 1);

            this.elements.RemoveAt(this.Size - 1);

            this.HeapifyDown(RootNodeIndex);

            return extracted;
        }

        private void HeapifyDown(int index)
        {
            Func<int, string, int> getChildIndex = (i, c) =>
            {
                if (c == "Left")
                {
                    return (i * 2) + 1;
                }
                else
                {
                    return (i * 2) + 2;
                }
            };
            Func<int, int, int> getBiggerChildIndex = (l, r) =>
            {
                if (this.IsGreater(l, r))
                {
                    return l;
                }

                return r;
            };

            int leftChildIndex = getChildIndex(index, "Left");
            int rightChildIndex = getChildIndex(index, "Right");

            while (this.Size > leftChildIndex)
            {
                if (leftChildIndex == this.Size - 1)
                {
                    if (this.IsGreater(leftChildIndex, index))
                        this.Swap(leftChildIndex, index);
                    break;
                }
                else
                {
                    var biggerChildIndex = getBiggerChildIndex(leftChildIndex, rightChildIndex);

                    this.Swap(index, biggerChildIndex);

                    index = biggerChildIndex;
                }

                leftChildIndex = getChildIndex(index, "Left");
                rightChildIndex = getChildIndex(index, "Right");
            }
        }

        public T Peek()
        {
            if (this.Size <= 0)
                throw new InvalidOperationException();

            return this.elements[0];
        }
    }
}
