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

namespace ParallelProgramming
{

    class Program
    {


        static void Main(string[] args)
        {
            //action bir delegate türüdür. değer döndürmeyen metodu temsil eder.
          var a= new Action((() => Console.WriteLine($"First {Task.CurrentId}")));
          var b= new Action((() => Console.WriteLine($"second {Task.CurrentId}")));
          var c= new Action((() => Console.WriteLine($"third {Task.CurrentId}")));

          Parallel.Invoke(a,b,c); //üçünü aynı anda çalıştırır

          ////////////////

          Parallel.For(1, 11, i =>
          {
              Console.WriteLine($"{i * i}\t");
          });

          //////////////////
          string[] words = {"selam", "ben", "hakan"};
          Parallel.ForEach(words, word =>
          {
              Console.WriteLine($"{word} has length {word.Length} (task {Task.CurrentId})");
          });
        }
    }
}
