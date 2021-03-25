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
            var items = ParallelEnumerable.Range(1, 20); //geriye paralel query döndürür
            var cts =new CancellationTokenSource();
            var results = items.Select(i =>
            {
                double result = Math.Log10(i);
             //   if(result>1) throw new InvalidOperationException(); //bu şekilde hata fırlatılabilir

                Console.WriteLine($"i={i}, tid={Task.CurrentId}");
                return result;
            });
            try
            {
                foreach (var x in results)
                {
                    if (x > 1)
                        cts.Cancel();

                    Console.WriteLine($"result={x}");
                }
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                    return true;
                });
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("canceled");
            }
        }
    }
}
