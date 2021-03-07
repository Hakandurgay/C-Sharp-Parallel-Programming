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
        //mutexler işletim sistemleri yapıları gibi farklı global processlerde yani farklı processler üzerinde çalışabilir ve bilgisayar ve uygulama seviyesinde lock mekanizması sunar.
        //farklı programlarda mutexler paylaşılabilir. Locklardan ayıran özelliği budur
        //lock mekanizmasına göre 50 kat daha yavaştır
        // ileri okuma için https://doganakyurek.blogspot.com/2016/08/multi-threading-serisi-ii.html adresine bakılabilir
        public int Balance  { get; set; }

        public void Deposit(int amount)
        {
            Balance += amount;
        }
        public void Withdraw(int amount)
        {
            Balance -= amount;
        }

        public void Transfer(BankAccount where, int amount)
        {
            Balance -= amount;
            where.Balance += amount;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var ba = new BankAccount();
            var ba2 = new BankAccount();
            var tasks=new List<Task>();
            
            Mutex mutex=new Mutex();  //mutex thread yönetim şekillerinden biridir. lock'tan farkı birden fazla processte çalışabilmesi
            Mutex mutex2 = new Mutex();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock= mutex.WaitOne();
                        try
                        {
                            ba.Deposit(1);

                        }
                        finally
                        {
                            if(haveLock)
                                mutex.ReleaseMutex();  //mutex'in sonlanması için releasemutex çağrılmalı
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex2.WaitOne();
                        try
                        {
                            ba2.Deposit(1);

                        }
                        finally
                        {
                            if (haveLock)
                                mutex2.ReleaseMutex();
                        }
                    }
                }));
                tasks.Add(Task.Factory.StartNew(()=>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = WaitHandle.WaitAll(new[] {mutex, mutex2}); //iki mutex de boşa çıkana kadar bekledikten sonra işlem yapar
                        try
                        {
                            ba.Transfer(ba2, 1);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"final balance is ba : {ba.Balance}");
            Console.WriteLine($"final balance is ba2: {ba2.Balance}");
        }
    }
}
