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
            var stack=new ConcurrentStack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            int result;
            if(stack.TryPeek(out result))
                Console.WriteLine($"{result} is on top");

            if(stack.TryPop(out result))
                Console.WriteLine($"popped {result}");

            var items = new int[5];
            if (stack.TryPopRange(items, 0, 5) > 0) //trypoprange verilen ilk parametredeki indexten itibaren ve ikinci parametreye kadar siler. kaç tane sildiğini döndüren int değeri döndürür. // burada 3 yani
            {
                var text = string.Join(", ", items.Select(i => i.ToString()));
                Console.WriteLine($"popped these items: {text}");
            }
        }
    }
}
