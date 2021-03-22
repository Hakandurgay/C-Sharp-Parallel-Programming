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
            //diğer senkronizasyon türlerinde counter ya arttırma ya da azaltma şeklinde çalışıyordu.
            //semaphorelarda iki şekilde de olabilir
            var semaphore=new SemaphoreSlim(2,10);//2, diğerlerindeki gibi number of requesti belirtiyor. yani eş zamanlı iki task
            //10 ise aynı anda çalışabailecek task sayısını ifade eder.
            for (int i = 0; i < 20; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"entering task {Task.CurrentId}");
                    semaphore.Wait(); //releasecount-- azalır

                    Console.WriteLine($"processing task {Task.CurrentId}");
                });
            }

            while (semaphore.CurrentCount <= 2)
            {
                Console.WriteLine($"Semaphore count : {semaphore.CurrentCount}");
                Console.ReadKey();
                semaphore.Release(2); //relasecount+=2
                //düğmeye her basıldığında yukarıdaki iki olan sayı iki artacak
            }
        }
    }
}
