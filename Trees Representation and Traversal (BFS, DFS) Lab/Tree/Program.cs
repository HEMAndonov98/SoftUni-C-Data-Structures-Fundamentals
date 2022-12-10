namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            var tree =  new Tree<int>(7,
                        new Tree<int>(19,
                                new Tree<int>(1),
                                new Tree<int>(12),
                                new Tree<int>(31)),
                        new Tree<int>(21),
                        new Tree<int>(14,
                                new Tree<int>(23),
                                new Tree<int>(6)));

            Console.WriteLine(string.Join(", ", tree.OrderBfs()));
            tree.Swap(14, 6);
            Console.WriteLine(string.Join(", ", tree.OrderBfs()));
        }
    }
}
