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

        public static void Demo()
        {

            var cts=new CancellationTokenSource();

            ParallelOptions po=new ParallelOptions();
            po.CancellationToken = cts.Token;
           var result= Parallel.For(0, 20,po ,(x, state) =>  //state bazı özelleştirmeler sağlar
            {
                Console.WriteLine($"{x}[{Task.CurrentId}]\t");
                if (x == 10)
                {
                  //  throw new Exception();
                   // state.Stop();  //x = 10 olduğunda paralel işlemleri durdurur
                //      state.Break(); //stopa benzer ama x=10 u görünce sonraki iterasyonları durdurur. stop'tan daha az acil
                cts.Cancel();
                }
            });
           Console.WriteLine();
           Console.WriteLine($"was loop completed?{result.IsCompleted} "); //tamamlanmışsa true eğer durdurulmuşsa veya hata fırlatılmışsa false
           if(result.LowestBreakIteration.HasValue)
               Console.WriteLine($"lowest break iteration is {result.LowestBreakIteration}");
        }
        static void Main(string[] args)
        {
            try
            {
                Demo();
            }
            catch(OperationCanceledException)
            {

            }
         
        }
    }
}
