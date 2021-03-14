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
   
    class Program{
    
        static void Main(string[] args)
        {
            // TPL'de concurrentList yok. bunun yerine ConcurrentBag var
            //stack lifo
            //queue fifo
            //bag: unorder

            var bag=new ConcurrentBag<int>();
            var tasks= new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var i1 = i;
                
                tasks.Add(Task.Factory.StartNew(()=>
                {
                    bag.Add(i1);
                    Console.WriteLine($"{Task.CurrentId} has added {i1}");
                    int result;
                    if (bag.TryPeek(out result))
                    {
                        Console.WriteLine($"{Task.CurrentId} has peeked the value {result}");
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            //sıra yok düzensiz oluyor her şey ama hızlı

            int last;
            if (bag.TryTake(out last))
            {
                Console.WriteLine($"i got {last}");
            }


        }
    }
}
