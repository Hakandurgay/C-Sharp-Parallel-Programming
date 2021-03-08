using System;
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
       static Random random=new Random();
        //çoklu okuma ve yazma işlemlerine özel olarak tasarlanmış lock sınıfıdır.
        //
        static ReaderWriterLockSlim padLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion); //
      //  static ReaderWriterLockSlim padLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion); yazılarak iç içe iki tane lock kullanılmak isteniyorsa yazılır 
        static void Main(string[] args)
        {
            int x = 0;
            var tasks=new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew((() =>
                {
                    padLock.EnterReadLock();

                    //eğer bir şey okunduktan sonra değiştirilmesi istenirse padlock.enterWriteLock yazılırsa readlock kapatılmadığı için hata verir.
                    //okunup değiştirilmesi isteniyorsa 
                    //  padLock.EnterUpgradeableReadLock();  yazılır
                    Console.WriteLine($"entered read lock, x={x}");
                    Thread.Sleep(5000);
                    padLock.ExitReadLock();

                    //padLock.ExitUpgradeableReadLock();
                    Console.WriteLine($"exited read lock, x={x}");
                })));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                });
            }

            while (true)
            {
                Console.ReadKey();
                padLock.EnterWriteLock();
                Console.Write("write lock acquired");
                int newValue = random.Next();
                x = newValue;
                Console.WriteLine($"set x={x}");
                padLock.ExitReadLock();
                Console.WriteLine("write lock released ");
            }

        }
    }
}
