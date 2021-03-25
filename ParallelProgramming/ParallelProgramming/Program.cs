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
        [Benchmark] //benchmark işlemle ilgili veriler veriyor
        public void SquareEachValue()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            Parallel.ForEach(values, x => { results[x] = (int)Math.Pow(x, 2); }); //her seferinde delegate ile işlemler yapıldığı için çok maliyetli bu şekilde yapmak

        }
        [Benchmark]
        public void SquareEachValueChunked()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            //countu böler
            var part = Partitioner.Create(0, count, 10000); //bu şekilde yapmak daha hızlı sonuç verir
            Parallel.ForEach(part, range =>
            {
                for (int i = 0; i < range.Item2; i++)
                {
                    results[i] = (int) Math.Pow(i, 2);
                }
            });


        }
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
            Console.WriteLine(summary);
        }
    }
}
