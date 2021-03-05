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

        public static int TextLenght(object o)
        {
            Console.WriteLine(($"Task with id {Task.CurrentId } processing object {o}...")); //current id çalışan task numarasını verir
            return o.ToString().Length;
        }

        static void Main(string[] args)
        {
            string text1 = "testing", text2 = "this";

            var task1=new Task<int>(TextLenght,text1); // bu şekilde veya factory kullanılarak task oluşturulabilir.
            task1.Start();

            Task<int> task2 =Task<int>.Factory.StartNew(TextLenght, text2);  //otomatik olarak başlatmaması için task 2 ye atandı

            Console.WriteLine($"Length of '{text1}' is {task1.Result}");  //result kullanınca taskin bitmesini bekler ve bitince sonucu yazdırır
            Console.WriteLine($"Length of '{text2}' is {task2.Result}");


            Console.ReadKey();
        }
    }
}
