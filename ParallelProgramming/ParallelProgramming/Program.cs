using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    class Program
    {
        public static void Write(char c)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(c);
            }
        }
        static void Main(string[] args)
        {
            Task.Factory.StartNew(() => Write('.'));

            var t=new Task(()=>Write('?'));
            t.Start();


            Write('-');

            Console.ReadKey();
        }
    }
}
