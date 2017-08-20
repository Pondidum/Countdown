using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Countdown
{
	class Program
	{
		private const string OutputFile = "./countdown.txt";

		static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				Console.WriteLine("Usage: ./countdown {time}");
				Console.WriteLine("e.g. ./countdown 16:30");
				return;
			}

			Func<DateTime> now = () => DateTime.Now;

			var parser = new TimeParser(now);
			var runTo = parser.Parse(args.First());

			if (runTo < now())
			{
				Console.WriteLine($"Time must be in the future (was {runTo})");
				return;
			}

			Console.WriteLine("Countdown will finish at:");
			Console.WriteLine($"{runTo:t} Local Time");
			Console.WriteLine($"{runTo.ToUniversalTime():t} UTC");

			var x = Console.CursorLeft;
			var y = Console.CursorTop;
			TimeSpan remaining;

			while ((remaining = runTo.Subtract(now())) > TimeSpan.Zero)
			{
				var time = $"{remaining.Minutes:00}:{remaining.Seconds:00}";

				Console.SetCursorPosition(x, y);
				Console.WriteLine(time + "            ");
				File.WriteAllText(OutputFile, time);

				Thread.Sleep(300);
			}

			Console.SetCursorPosition(x, y);
			Console.WriteLine("Stream starting soon...       ");
			File.WriteAllText(OutputFile, "Stream starting soon...");
		}
	}
}
