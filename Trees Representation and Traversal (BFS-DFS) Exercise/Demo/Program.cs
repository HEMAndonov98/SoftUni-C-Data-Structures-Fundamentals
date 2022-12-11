namespace Demo
{
    using System;
    using Tree;

    class Program
    {
        static void Main(string[] args)
        {
            string[] input = new string[]
              {
                "7 19", "7 21", "7 14", "19 1", "19 12", "19 31", "14 23", "14 6"
              };

            var factory = new IntegerTreeFactory();
            var tree = factory.CreateTreeFromStrings(input);
            //Console.WriteLine(string.Join(", ", tree.GetInternalKeys()));
            //Console.WriteLine(tree.GetDeepestKey());
            //Console.WriteLine(string.Join(", ", tree.GetLongestPath()));
            foreach (var path in tree.GetPathsWithGivenSum(27))
            {
                Console.WriteLine(string.Join(", ", path));
            }
        }
    }
}
