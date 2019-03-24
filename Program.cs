using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labaratorinis_01_HeapSort_D
{
    class Program
    {
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
        
            Test_File_Array_List(seed);
        }

        public static void HeapSort(DataArray items)
        {
            int heapSize = items.Length - 1;
            for (int i = heapSize / 2; i >= 0; i--)
            {
                Heapify(items, heapSize, i);
            }
            for (int i = items.Length - 1; i > 0; i--)
            {
                items.Swap(0, items[0], i, items[i]);
                heapSize--;
                Heapify(items, heapSize, 0);
            }
        }

        public static void HeapSort(DataList items)
        {
            int heapSize = items.Length - 1;
            for (int i = heapSize / 2; i >= 0; i--)
            {
                HeapifyList(items, heapSize, i);
            }
            for (int i = items.Length - 1; i > 0; i--)
            {
                items.Swap(0, i);
                heapSize--;
                HeapifyList(items, heapSize, 0);
            }
        }

        public static void Heapify(DataArray items, int heapSize, int index)
        {
            int left = index * 2;
            int right = index * 2 + 1;
            int largest = 0;

            if (left <= heapSize && items[left] > items[index])
            {
                largest = left;
            }
            else largest = index;

            if (right <= heapSize && items[right] > items[largest])
            {
                largest = right;
            }

            if (largest != index)
            {
                items.Swap(index, items[index], largest, items[largest]);
                Heapify(items, heapSize, largest);
            }
        }

        public static void HeapifyList(DataList items, int heapSize, int index)
        {
            int left = index * 2;
            int right = index * 2 + 1;
            int largest = 0;

            if (left <= heapSize && items.getData(left) > items.getData(index))
            {
                largest = left;
            }
            else largest = index;

            if (right <= heapSize && items.getData(right) > items.getData(largest))
            {
                largest = right;
            }

            if (largest != index)
            {
                items.Swap(index, largest);
                HeapifyList(items, heapSize, largest);
            }
        }

        public static void Test_File_Array_List(int seed)
        {
            int n = 100;
            string filename;
            filename = @"mydataarray.dat";
            Console.WriteLine("Heap sort isorineje atmintyje:");
            Console.WriteLine("Duomenu masyvas:");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("| Elementu kiekis |  Rikiavimo laikas |");
            Console.WriteLine("|-----------------|-------------------|");
            for (int i = 0; i < 7; i++)
            {
                MyFileArray myfileArray = new MyFileArray(filename, n, seed);
                using (myfileArray.fs = new FileStream(filename, FileMode.Open,
                    FileAccess.ReadWrite))
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    HeapSort(myfileArray);
                    watch.Stop();
                    Console.WriteLine("| {0,15} | {1,17} |", n, watch.Elapsed);
                    n = n * 2;
                    watch.Reset();
                }
            }
            Console.WriteLine("|-----------------|-------------------|");

            n = 100;
            filename = @"mydatalist.dat";
            Console.WriteLine("Duomenu susietas sarasas:");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("| Elementu kiekis |  Rikiavimo laikas |");
            Console.WriteLine("|-----------------|-------------------|");
            for (int i = 0; i < 7; i++)
            {
                MyFileList myfileList = new MyFileList(filename, n, seed);
                using (myfileList.fs = new FileStream(filename, FileMode.Open,
                    FileAccess.ReadWrite))
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    HeapSort(myfileList);
                    watch.Stop();
                    Console.WriteLine("| {0,15} | {1,17} |", n, watch.Elapsed);
                    n = n * 2;
                    watch.Reset();
                }
            }
            Console.WriteLine("|-----------------|-------------------|");
        }
    }

    abstract class DataArray
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract double this[int index] { get; }
        public abstract void Swap(int indA, double a, int indB, double b);
        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(" {0:F5} ", this[i]);
            }
            Console.WriteLine();
        }
    }

    abstract class DataList
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract double Head();
        public abstract double Next();
        public abstract double getData(int index);
        public abstract void Swap(int a, int b);
        public void Print(int n)
        {
            Console.WriteLine(" {0:F5} ", Head());
            for (int i = 1; i < n; i++)
            {
                Console.WriteLine(" {0:F5} ", Next());
            }
            Console.WriteLine();
        }
    }
}
