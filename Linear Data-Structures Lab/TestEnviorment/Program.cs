
internal class Program
{
    private static void Main(string[] args)
    {
        {
            var testarr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var myStack = new Problem02.Stack.Stack<int>();
            var builtInStack = new Stack<int>();

            foreach (var item in testarr)
            {
                myStack.Push(item);
                builtInStack.Push(item);
            }
            Console.WriteLine();
        }
    }
}