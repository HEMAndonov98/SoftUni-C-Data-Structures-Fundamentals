
internal class Program
{
    private static void Main(string[] args)
    {
        {
            var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var list = new Problem01.List.List<int>();
            int itemToRemove = expected[expected.Length / 2];
            foreach (var num in expected)
            {
                list.Add(num);
            }
            list.Remove(itemToRemove);
        }
    }
}