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
        //2 burada kaç tane participant olduğunu söylüyor yani water and cup.
        //phase cup or water
        static Barrier barrier=new Barrier(2, b =>
        {
            Console.WriteLine($"phase {b.CurrentPhaseNumber} is finished");
        });

        public static void Water()
        {
            Console.WriteLine("putting the kettle on (takes a bit longer)");
            Thread.Sleep(2000);
            barrier.SignalAndWait();
            Console.WriteLine("pouring water into cup");
            barrier.SignalAndWait();
            Console.WriteLine("putting the kettle away");

        }

        public static void Cup()
        {
            Console.WriteLine("finding the cup of the (fast)");
            barrier.SignalAndWait();
            Console.WriteLine("adding tea");
            barrier.SignalAndWait();
            Console.WriteLine("adding sugar");

        }
        static void Main(string[] args)
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);

            var tea = Task.Factory.ContinueWhenAll(new[] {water, cup}, tasks =>
            {
                Console.WriteLine("enjoy your cup of the");
            });
            tea.Wait();
             
        }
    }
}
