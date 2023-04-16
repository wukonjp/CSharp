using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            OutThereadID("Main:前");

			Sub1Async();
			OutThereadID("Main:中");

			Sub2Async();
			OutThereadID("Main:後");

			Console.WriteLine("Mainが完了しました");
			Console.ReadLine();
        }

        private static async Task Sub1Async()
        {
            OutThereadID("Sub1Async:前");

			await Task.Delay(100);
            OutThereadID("Sub1Async:中");

			await Task.Delay(100);
			OutThereadID("Sub1Async:後");
		}

		private static async Task Sub2Async()
		{
			OutThereadID("Sub2Async:前");

			await Task.Delay(100);
			OutThereadID("Sub2Async:中");

			await Task.Delay(100);
			OutThereadID("Sub2Async:後");
		}

		private static void OutThereadID(string name)
        {
            var id = System.Threading.Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("{0}、ID:{1}", name, id);
        }
    }
}
