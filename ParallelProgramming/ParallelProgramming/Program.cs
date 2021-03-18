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
            #region continueWith Kullanımı

            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("boiling water");
            });
            var task2 = task.ContinueWith(t =>
            {
                Console.WriteLine($"completed task {t.Id}");  //ilk task biter bitmek sonraki task ile devam eder
            });
            task2.Wait();

            #endregion

            #region continuewhenall kullanımı
            //var taskk = Task.Factory.StartNew(() => "task 1");
            //var taskk2 = Task.Factory.StartNew(() => "task 2");

            ////continuewhenall bütün taskleri bekler
            //var taskk3 = Task.Factory.ContinueWhenAll(new[] { taskk, taskk2 }, tasks =>
            //{
            //    Console.WriteLine("tasks completed");
            //    foreach (var t in tasks)
            //    {
            //        Console.WriteLine("-- " + t.Result);
            //    }
            //});

            #endregion

            var taskk = Task.Factory.StartNew(() => "task 1");
            var taskk2 = Task.Factory.StartNew(() => "task 2");

            //continuewhenany bir herhangi bir taskin bitmesini bekler
            var taskk3 = Task.Factory.ContinueWhenAny(new[] { taskk, taskk2 }, t =>
            {
                Console.WriteLine("tasks completed");
            
                    Console.WriteLine("-- " + t.Result);
            });


        }
    }
}
