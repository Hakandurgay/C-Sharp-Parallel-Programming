using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace ParallelProgramming
{

  public  class Program
    {
      
        static void Main(string[] args)
        {
            const int count = 50;
            var items = Enumerable.Range(1, count).ToArray();
            var results = new int[count];

            //AsParallel extension metodu kullanılarak paralel linq sorgusu oluşturulur
            items.AsParallel().ForAll(x =>  //forall geriye değer döndürmez 
            {
                int newValue = x * x * x;
                Console.WriteLine($"{newValue} ({Task.CurrentId})\t");
                results[x - 1] = newValue;
            });
            Console.WriteLine();
            Console.WriteLine();

            var cubes = items.AsParallel().AsOrdered().Select(x => x * x * x);  //asordered kullanılarak sıra sağlanır
            foreach (var i in cubes)
            {
                Console.WriteLine($"{i}\t");
            }
            Console.WriteLine();
        }
    }
}
