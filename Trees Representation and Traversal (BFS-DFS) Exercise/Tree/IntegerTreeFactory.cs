namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IntegerTreeFactory
    {
        private Dictionary<int, IntegerTree> nodesByKey;

        public IntegerTreeFactory()
        {
            this.nodesByKey = new Dictionary<int, IntegerTree>();
        }

        public IntegerTree CreateTreeFromStrings(string[] input)
        {
            foreach (var inputLine in input)
            {
                var treeParams = inputLine.Split().Select(int.Parse).ToArray();

                var parent = treeParams[0];
                var child = treeParams[1];

                this.AddEdge(parent, child);
            }

            return this.GetRoot();
        }

        public IntegerTree CreateNodeByKey(int key)
        {
            if (!this.nodesByKey.ContainsKey(key))
            {
                this.nodesByKey[key] = new IntegerTree(key);
            }

            return this.nodesByKey[key];
        }

        public void AddEdge(int parent, int child)
        {
            var parentTree = this.CreateNodeByKey(parent);
            var childTree = this.CreateNodeByKey(child);

            parentTree.AddChild(childTree);
            childTree.AddParent(parentTree);
        }

        public IntegerTree GetRoot()
            => this.nodesByKey.FirstOrDefault(kvp => kvp.Value.Parent == null).Value;
    }
}
