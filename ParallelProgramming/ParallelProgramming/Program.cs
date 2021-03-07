using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ParallelProgramming
{

    public class BankAccount
    {
        //birden basla thread t anında n tane işlem yapabilme ihtimali olduğu için lock keywordu uygulanmazsa critical section oluşur. yani 
        // program her çalıştığında sonuç sıfır olması gerekirken farklı sonuçlar elde edilebilirç
        //bunu önlemek için lock keywordu kullanılır. Bir thread içeri girdiğinde diğeri de girmek isterse ilk giren bitene kadar bekler.
        //böylece hepsi sırayla girerek bir işlemde birden fazla thread çalışması engellenir.
        public object padlock=new object();
        public int Balance  { get; set; }

        public void Deposit(int amount)
        {
            lock (padlock)
            {
                Balance += amount;
            }
        }

        public void Withdraw(int amount)
        {
            lock (padlock)
            {
              Balance -= amount;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var ba = new BankAccount();
            var tasks=new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Deposit(100);
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"final balance is: {ba.Balance}");
        }
    }
}
