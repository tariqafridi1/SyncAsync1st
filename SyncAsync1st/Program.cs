using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        int[] arr = GenerateRandomArray(10000);

        Console.WriteLine("Number of Elements in Array: " + arr.Length);

        // Sync Sorting
        Console.WriteLine("\nSynchronous Sorting:");

        int[] syncBubbleSortArray = new int[arr.Length];
        Array.Copy(arr, syncBubbleSortArray, arr.Length);
        MeasureSortingTime("Bubble Sort (Synchronous)", () => SynchronousBubbleSort(syncBubbleSortArray));

        int[] syncQuickSortArray = new int[arr.Length];
        Array.Copy(arr, syncQuickSortArray, arr.Length);
        MeasureSortingTime("Quick Sort (Synchronous)", () => SynchronousQuickSort(syncQuickSortArray, 0, syncQuickSortArray.Length - 1));

        // Async Sorting
        Console.WriteLine("\nAsynchronous Sorting:");

        int[] asyncBubbleSortArray = new int[arr.Length];
        Array.Copy(arr, asyncBubbleSortArray, arr.Length);
        await MeasureSortingTimeAsync("Bubble Sort (Asynchronous)", () => AsynchronousBubbleSort(asyncBubbleSortArray));

        int[] asyncQuickSortArray = new int[arr.Length];
        Array.Copy(arr, asyncQuickSortArray, arr.Length);
        await MeasureSortingTimeAsync("Quick Sort (Asynchronous)", () => AsynchronousQuickSort(asyncQuickSortArray, 0, asyncQuickSortArray.Length - 1));

        Console.ReadLine();
    }

    static void SynchronousBubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }
    static void MeasureSortingTime(string algorithmName, Action sortingAction)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        sortingAction.Invoke();
        stopwatch.Stop();
        Console.WriteLine($"{algorithmName}: {stopwatch.ElapsedMilliseconds}ms");
    }




    static void SynchronousQuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr, low, high);
            SynchronousQuickSort(arr, low, pivotIndex - 1);
            SynchronousQuickSort(arr, pivotIndex + 1, high);
        }
    }

    
    static async Task MeasureSortingTimeAsync(string algorithmName, Func<Task> sortingActionAsync)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        await sortingActionAsync.Invoke();
        stopwatch.Stop();
        Console.WriteLine($"{algorithmName}: {stopwatch.ElapsedMilliseconds}ms");
    }

    static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j <= high - 1; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        int temp1 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp1;

        return i + 1;
    }

    static async Task<int> PartitionAsync(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j <= high - 1; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        int temp1 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp1;

        return i + 1;
    }

    static int[] GenerateRandomArray(int size)
    {
        Random rand = new Random();
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = rand.Next(1, 1000); 
        }
        return arr;
    }

    
    static async Task AsynchronousBubbleSort(int[] array)
    {
        await Task.Run(() => SynchronousBubbleSort(array));
    }

   
    static async Task AsynchronousQuickSort(int[] arr, int low, int high)
    {
        await Task.Run(() => Partition(arr,low,high));
    }
}