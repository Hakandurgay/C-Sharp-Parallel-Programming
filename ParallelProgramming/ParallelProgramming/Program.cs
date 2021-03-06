using System;
using System.Collections.Generic;
using System.Linq;
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
         
            //bu üç token paranoid tokena bağlı. bu üçünden biri cancel edilirse paranoid token hata fırlatır
          var planned= new CancellationTokenSource();   
          var preventative= new CancellationTokenSource();
          var emergency= new CancellationTokenSource();

          var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
              planned.Token, preventative.Token, emergency.Token);

          Task.Factory.StartNew(() =>
          {
              int i = 0;
              while (true)
              {
                  paranoid.Token.ThrowIfCancellationRequested();
                  Console.WriteLine($"{i++}\t");
              }
          },paranoid.Token);

          Console.ReadKey();
          emergency.Cancel();

          Console.ReadKey();
        }
    }
}
