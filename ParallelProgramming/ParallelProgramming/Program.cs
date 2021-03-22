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
          var evt=new ManualResetEventSlim(false);

          Task.Factory.StartNew(() =>
          {
              Console.WriteLine("boiling water");
              //counter 1 olur
              evt.Set();  //barrierde ve countdownevette counter geriye doğru sıfıra kadar akıyordu. manualresetevet'te 0'dan bire doğru gidiyor.
              //true işaretler
          });
          var makeTea = Task.Factory.StartNew(() =>
          {
              Console.WriteLine("waiting for water");
              //counter 1 olana kadar bekler ondan sonra aşağıyı çalıştır
              evt.Wait(); //true devam eder.
              Console.WriteLine("here is your tea");
              evt.Wait(); 
              evt.Wait(); 
              evt.Wait(); 
              evt.Wait(); 
              evt.Wait(); //buraya istediğimiz kadar wait yazalım yine de beklemez çünkü true olarak set edildi
          });
          makeTea.Wait();
        }
    }
}
