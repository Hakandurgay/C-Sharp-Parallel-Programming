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
            var cts = new CancellationTokenSource();  //task'den çıkılmasını sağlar
            var token = cts.Token;

            token.Register(() =>
            {
                Console.WriteLine("Cancelation has been requested");
            });

            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {

                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t ");


                    #region task'den bu şekilde de çıkılabilir ama yukarıdaki daha uygun

                    //if (token.IsCancellationRequested)
                    //    break;
                    //else
                    //{
                    //    Console.WriteLine($"{i++}\t ");
                    //}

                    #endregion

                }
            }, token);
            t.Start();

            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne(); //yukarıdaki task beklemeye alınınca bu çalışır.
                Console.WriteLine("wait handle çalıştı. task'dan çıkıldı");
            });

            Console.ReadKey();
            cts.Cancel();  //klavyeden değer okumadan sonra cts.cansel ile task durdurulur

            Console.ReadKey();
            Console.ReadKey();




        }
    }
}
