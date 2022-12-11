namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private LinkedList<Tree<T>> children;

        public Tree(T key)
        {
            this.Key = key;
            children = new LinkedList<Tree<T>>();
        }

        public Tree(T key, params Tree<T>[] children)
            :this(key)
        {
            foreach (var child in children)
            {
                this.AddChild(child);
                child.AddParent(this);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => this.children;

        public void AddChild(Tree<T> child)
        {
            this.children.AddLast(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string AsString()
        {
            var indentation = 0;
            var sb = new StringBuilder();
            this.DfsAsString(this, sb, indentation);

            return sb.ToString().Trim();
        }

        private void DfsAsString(Tree<T> tree, StringBuilder sb, int indentation)
        {
            sb.Append(' ', indentation);
            sb.AppendLine(tree.Key.ToString());

            foreach (var subtree in tree.children)
            {
                this.DfsAsString(subtree, sb, indentation + 2);
            }

        }

        public IEnumerable<T> GetInternalKeys()
        {
            var result = new List<Tree<T>>();
            return this.TraverseDfs(tree => tree.Children.Count > 0 && tree.Parent != null, this, result).Select(tree => tree.Key);
        }

        public IEnumerable<T> GetLeafKeys()
        {
            var result = new List<Tree<T>>();
            return this.TraverseDfs(tree => tree.Children.Count == 0, this, result).Select(tree => tree.Key);
        }
        private IEnumerable<Tree<T>> TraverseDfs(Predicate<Tree<T>> predicate, Tree<T> tree, List<Tree<T>> result)
        {
            if (predicate.Invoke(tree))
            {
                result.Add(tree);
            }

            foreach (var child in tree.children)
            {
                this.TraverseDfs(predicate, child, result);
            }

            return result;
        }

        public T GetDeepestKey()
            => this.GetDeepestLeaf().Key;

        private Tree<T> GetDeepestLeaf()
        {
            var maxDepth = 0;
            Tree<T> deepestLeaf = null;

            var leafs = new List<Tree<T>>();
            this.TraverseDfs(tree => tree.Children.Count == 0, this, leafs).ToList();

            foreach (var leaf in leafs)
            {
                int depth = this.GetDepth(leaf);

                if (depth > maxDepth)
                {
                    maxDepth = depth;
                    deepestLeaf = leaf;
                }
            }

            return deepestLeaf;
        }

        private int GetDepth(Tree<T> leaf)
        {
            var result = 0;
            while (leaf.Parent != null)
            {
                result++;
                leaf = leaf.Parent;
            }

            return result;
        }

        public IEnumerable<T> GetLongestPath()
        {
            var deepestLeaf = this.GetDeepestLeaf();
            var path = new Stack<T>();

            while (deepestLeaf != null)
            {
                path.Push(deepestLeaf.Key);
                deepestLeaf = deepestLeaf.Parent;
            }

            return path;
        }
    }
}
