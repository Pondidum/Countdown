using System;
using System.Linq;
using System.Threading;

namespace Countdown
{
	class Program
	{
		static void Main(string[] args)
		{
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
				Console.SetCursorPosition(x, y);
				Console.WriteLine($"{remaining.Minutes:00}:{remaining.Seconds:00}            ");

				Thread.Sleep(300);
			}

			Console.SetCursorPosition(x, y);
			Console.WriteLine("Stream starting soon...       ");
		}
	}
}
