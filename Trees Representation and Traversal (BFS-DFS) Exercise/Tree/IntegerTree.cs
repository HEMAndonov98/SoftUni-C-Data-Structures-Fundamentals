namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IntegerTree : Tree<int>, IIntegerTree
    {
        public IntegerTree(int key, params Tree<int>[] children)
            : base(key, children)
        {
        }

        public IEnumerable<IEnumerable<int>> GetPathsWithGivenSum(int sum)
        {
            var paths = new List<Stack<int>>();
            var currentPath = new Stack<int>();
            return this.GetPathWithDfs(this, sum, paths, currentPath);
        }

        private IEnumerable<IEnumerable<int>> GetPathWithDfs(Tree<int> integerTree, int sum, List<Stack<int>> paths, Stack<int> path)
        {
            path.Push(integerTree.Key);

            if (path.Sum() == sum)
            {
                paths.Add(new Stack<int>(path));
            }

            foreach (var child in integerTree.Children)
            {
                this.GetPathWithDfs(child, sum, paths, path);
            }

            path.Pop();
            return paths;
        }

        public IEnumerable<Tree<int>> GetSubtreesWithGivenSum(int sum)
        {
            throw new NotImplementedException();
        }
    }
}
