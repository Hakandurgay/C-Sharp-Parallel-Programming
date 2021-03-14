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
            var q= new ConcurrentQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);

            int result;
            if (q.TryDequeue(out result))
            {
                Console.WriteLine($"removed element {result}");
            }

            if (q.TryPeek(out result))
            {
                Console.WriteLine($"front element is {result}");
            }
        }
    }
}
