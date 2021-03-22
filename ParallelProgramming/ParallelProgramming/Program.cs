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
          var evt=new AutoResetEvent(false);

          Task.Factory.StartNew(() =>
          {
              Console.WriteLine("boiling water");
              //counter 1 olur
              evt.Set(); 
              //true işaretler
          });
          var makeTea = Task.Factory.StartNew(() =>
          {
              Console.WriteLine("waiting for water");
              //counter 1 olana kadar bekler ondan sonra aşağıyı çalıştır
              evt.WaitOne(); //tekrar false 'a döndürür. 
              Console.WriteLine("here is your tea");
              var ok = evt.WaitOne(1000); //hala false
              evt.Set();
              if (ok)
              {
                  Console.WriteLine("enjoy your tea");
              }
              else
              {
                  Console.WriteLine("no tea");
              }

          });
          makeTea.Wait();
        }
    }
}
