using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Countdown
{
	class Program
	{
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

			var outputFile = Path.Combine(
				Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
				"countdown.txt");

			Console.WriteLine("");
			Console.WriteLine($"Writing to: {outputFile}");
			Console.WriteLine("");

			TimeSpan remaining;
			var lastTime = string.Empty;

			while ((remaining = runTo.Subtract(now())) > TimeSpan.Zero)
			{
				var time = $"{remaining.Minutes:00}:{remaining.Seconds:00}";

				if (time != lastTime)
				{
					File.WriteAllText(outputFile, time);
					lastTime = time;
				}

				Thread.Sleep(200);
			}

			Console.WriteLine("Done.");
			File.WriteAllText(outputFile, "Stream starting soon...");
		}
	}
}
