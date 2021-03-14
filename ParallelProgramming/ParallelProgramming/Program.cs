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
    //concurrentdictionary normal dictionary'nin thread safe şeklidir.
    //Thread Safe; birden çok thread’in tekbir kaynağı aynı anda kullanabilmesi/erişebilmesi durumlarında ortaya çıkabilecek tutarsızlıklar neticesinde üretilecek olası hatalara karşı o anki
    //thread’in kaynağını güvenceye alan ve bunu o kaynağı kullanan tüm threadler için sağlayan bir konsepttir.

    class Program
    {
        private static ConcurrentDictionary<string,string> capitals=new ConcurrentDictionary<string, string>();

        public static void AddParis()
        {
            bool success = capitals.TryAdd("France", "Paris"); //normal add metodununn yerine tryadd kullanır. bu method bool türündendir eğer eklenmiş olan veri daha önce varsa false döne
            string who=Task.CurrentId.HasValue ?  ("Task "+ Task.CurrentId) : "Main Thread";  //currentid nullabledır.
            Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element"); //ilk çağrımızda eklencek ama ikinci çağrışımızda eklenmicek
        }
        static void Main(string[] args)
        {

            Task.Factory.StartNew(AddParis).Wait(); //oluşturduğumuz thread
            AddParis(); //main thread

             capitals["Russia"] = "Leningrad";   //update işlemini bu şekilde de yapabilir ama concurrentdictionary'nin kendine has metodu var
            capitals.AddOrUpdate("Russia", "Moscow", (k, old) => old + "--> Moscow");
            //ilk parametreye önceden atanmış bir değer var mı diye bakar yoksa atar varsa update yapar
            Console.WriteLine($"the capital of russia is {capitals["Russia"]}");


             //capitals["Sweeden"] = "Uppsala";
            var capOfSweeden = capitals.GetOrAdd("Sweeden", "Stockholm"); //eğer önceden atanmış varsa onu getirir yoksa stockholm yapar
            Console.WriteLine($"the cap of sweeden is {capOfSweeden}");

            const string toRemove = "Russia";
            string removed;
            bool didRemove = capitals.TryRemove(toRemove, out removed);
            if (didRemove)
            {
                Console.WriteLine($"we just removed {removed}");
            }
            else
            {
                Console.WriteLine($"failed to remove the capital of {toRemove}");
            }
        }
    }
}
