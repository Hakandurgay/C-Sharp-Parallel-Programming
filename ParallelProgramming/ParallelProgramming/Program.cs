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
        //Interlocked.Add atomic olmayan işlemi arka planda atomic olarak yaptığı için lock ile aynı sonucu verir
        private int _balance;  //ref operatörü propertynin back fieldı olmadan kullanılamaz.

        public int Balance
        {
            get => _balance;
            set => _balance = value;
        }

        public void Deposit(int amount)
        {
            Interlocked.Add(ref _balance, amount);
            
         
        }

        public void Withdraw(int amount)
        {
            Interlocked.Add(ref _balance, -amount);
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
