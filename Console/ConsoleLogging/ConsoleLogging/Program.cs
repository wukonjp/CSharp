using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleLogging
{
	class Program
	{
		static void Main(string[] args)
		{
			Trace.AutoFlush = false;
			var listener = (DefaultTraceListener)Trace.Listeners["Default"];
			listener.LogFileName = "log.txt";

			Trace.WriteLine("0");
			Console.WriteLine("hit any key.");
			Console.ReadKey();

			Trace.WriteLine("1");
			Console.WriteLine("hit any key.");
			Console.ReadKey();

			Trace.WriteLine("2");
			Console.WriteLine("hit any key.");
			Console.ReadKey();

			Trace.WriteLine("3");
			Console.WriteLine("hit any key.");
			Console.ReadKey();
		}
	}
}
