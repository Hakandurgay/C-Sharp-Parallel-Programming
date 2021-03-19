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
            #region attached child task

            var parent = new Task(() =>
            {
               
                var child = new Task(() =>
                {
                    Console.WriteLine("child task starting");
                    Thread.Sleep(3000);
                    Console.WriteLine("child task finishing");
                    throw new Exception();
                }, TaskCreationOptions.AttachedToParent);  //bu eklenerek parent taskin child taski beklemesi sağlanır

                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"good, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);  //bu şekilde birden fazla koşul verebilir. burası sadece task başarılı olursa çalıır
                var failHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"bad, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);  //hataya düşerse burası çalışır


                child.Start();
            });
            parent.Start();
            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }


            #endregion

            #region detached child task
            /*      //bu örnekte farklı çıktılar oluşmasının sebebi parent task in child taskin bitmesini beklememesi

                  var parent = new Task(() =>
                  {
                      //bu şekilde oluşturmaya detached denir.bu durumda child task'in diğer taskin içinde olması veya farklı yerde olması farketmez.
                      //parent'ından bağımsız olarak execute edilir.
                      //parent childi beklemez, parentin statusu childa bağlı değildir, child task tarafdından fırlatılan exceptionlar parent tarafından bağımsız
                      var child = new Task(() =>
                      {
                          Console.WriteLine("child task starting");
                          Thread.Sleep(3000);
                          Console.WriteLine("child task finishing");
                      });
                      child.Start();
                  });
                  parent.Start();
                  try
                  {
                      parent.Wait();
                  }
                  catch (AggregateException ae)
                  {
                      ae.Handle(e => true);
                  }      
            */
            #endregion

        }
    }
}
