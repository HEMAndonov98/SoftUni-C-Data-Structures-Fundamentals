// See https://aka.ms/new-console-template for more information
using Problem01.CircularQueue;

var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

var realQueue = new Queue<int>();
var myQueue = new CircularQueue<int>();

foreach (var num in array)
{
    realQueue.Enqueue(num);
    myQueue.Enqueue(num);
}

foreach (var val in myQueue)
{
    Console.WriteLine(val);
}
