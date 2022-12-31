namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T element, IAbstractBinaryTree<T> left, IAbstractBinaryTree<T> right)
        {
            this.Value = element;
            this.LeftChild = left;
            this.RightChild = right;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            var sb = new StringBuilder();
            return this.PreOrderDFS(indent, this, sb);
        }

        private string PreOrderDFS(int indent, IAbstractBinaryTree<T> binaryTree, StringBuilder sb)
        {
            if (binaryTree != null)
            {
                sb.Append(' ', indent);
                sb.AppendLine(binaryTree.Value.ToString());
                this.PreOrderDFS(indent + 2, binaryTree.LeftChild, sb);
                this.PreOrderDFS(indent + 2, binaryTree.RightChild, sb);
            }

            return sb.ToString().Trim();
        }

        public void ForEachInOrder(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IAbstractBinaryTree<T>> InOrder()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IAbstractBinaryTree<T>> PostOrder()
        {
            throw new NotImplementedException();
        }

        public  IEnumerable<IAbstractBinaryTree<T>> PreOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            //Two different methods for creating an IEnumerable in PreOrder for Binary Tree 

            //result.Add(this);

            //if (this.LeftChild != null)
            //{
            //    result.AddRange(this.LeftChild.PreOrder());
            //}

            //if (this.RightChild != null)
            //{
            //    result.AddRange(this.RightChild.PreOrder());
            //}

            //return result;

            yield return this;

            if (this.LeftChild != null)
            {
                foreach (var item in this.LeftChild.PreOrder())
                {
                    yield return item;
                }
            }

            if (this.RightChild != null)
            {
                foreach (var item in this.RightChild.PreOrder())
                {
                    yield return item;
                }
            }

        }
    }
}
