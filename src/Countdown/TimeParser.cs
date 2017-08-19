using System;

namespace Countdown
{
	public class TimeParser
	{
		private readonly Func<DateTime> _now;

		public TimeParser(Func<DateTime> now)
		{
			_now = now;
		}

		public DateTime Parse(string entered)
		{
			var span = TimeSpan.Parse(entered);
			var now = _now();

			return now
				.Subtract(now.TimeOfDay)
				.Add(span);
		}
	}
}
