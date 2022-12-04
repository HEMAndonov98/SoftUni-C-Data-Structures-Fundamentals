using Problem03.ReversedList;

var list = new ReversedList<int>();
var numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
int itemToRemove = numbers[numbers.Length / 2];
foreach (var num in numbers)
{
    list.Add(num);
}
list.RemoveAt(3);


Console.WriteLine();