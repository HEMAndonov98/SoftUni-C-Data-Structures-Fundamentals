namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        private T value;
        private Tree<T> parent;
        private List<Tree<T>> children;

        public Tree(T value)
        {
            this.value = value;
            this.children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.parent = this;
                this.children.Add(child);
            }
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var parent = this.Dfs(parentKey);

            if (parent == null)
            {
                throw new ArgumentNullException();
            }

            child.parent = parent;
            parent.children.Add(child);
        }

        public IEnumerable<T> OrderBfs()
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node.value;

                foreach (var child in node.children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        public IEnumerable<T> OrderDfs()
            => this.DfsHelper(this, new List<T>());

        public void RemoveNode(T nodeKey)
        {
            var nodeToBeRemoved = this.Bfs(nodeKey);

            if (nodeToBeRemoved == null)
            {
                throw new ArgumentNullException();
            }

            var toBeRemovedParent = nodeToBeRemoved.parent;

            if (toBeRemovedParent == null)
            {
                throw new ArgumentException();
            }

            toBeRemovedParent.children.Remove(nodeToBeRemoved);
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.Dfs(firstKey);
            var secondNode = this.Dfs(secondKey);

            if (firstNode == null | secondNode == null)
            {
                throw new ArgumentNullException();
            }

            var firstNodeParent = firstNode.parent;
            var secondNodeParent = secondNode.parent;

            if (firstNode.parent == null | secondNode.parent == null)
            {
                throw new ArgumentException();
            }

            var indexOfFirstNode = firstNodeParent.children.IndexOf(firstNode);
            var indexOfSecondNode = secondNodeParent.children.IndexOf(secondNode);

            firstNodeParent.children[indexOfFirstNode] = secondNode;
            secondNode.parent = firstNodeParent;

            secondNodeParent.children[indexOfSecondNode] = firstNode;
            firstNode.parent = secondNodeParent;
        }

        private IEnumerable<T> DfsHelper(Tree<T> node, List<T> result)
        {
            foreach (var child in node.children)
            {
                this.DfsHelper(child, result);
            }

            result.Add(node.value);
            return result;
        }

        //I have implemented Dfs and Bfs twice to practice them
        private Tree<T> Dfs(T parentKey)
        {
            var stack = new Stack<Tree<T>>();
            stack.Push(this);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                if (node.value.Equals(parentKey))
                {
                    return node;
                }

                foreach (var child in node.children)
                {
                    stack.Push(child);
                }
            }

            return null;
        }

        private Tree<T> Bfs(T nodeKey)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node.value.Equals(nodeKey))
                {
                    return node;
                }

                foreach (var child in node.children)
                {
                    queue.Enqueue(child);
                }
            }
            return null;
        }
    }
}
