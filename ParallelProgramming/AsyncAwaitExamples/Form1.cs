using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncAwaitExamples
{
    public partial class Form1 : Form
    {
        public async Task<int> CalculateValueAsync() //bu şekilde yapılırsa bekleme işlemi olurken ui kısmında işlem yapılabilir. 
        {
          await  Task.Delay(5000);   //async arkaplanda özellikle bir iş yapmaz sadece await'in kullanılacağını belirtir
            return  123;
        }
        public Form1()
        { 
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var calculation =await CalculateValueAsync();
            label1.Text = calculation.ToString();

            //await kullanıldığı için bu şekilde uzun yazmaya gerek kalmadı
            //calculation.ContinueWith(t =>
            //{
            //    label1.Text = t.Result.ToString();

            //}, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
