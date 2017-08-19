using Shouldly;
using System;
using Xunit;

namespace Countdown.Tests
{
	public class TimeParserTests
	{
		[Theory]
		[InlineData("2017/08/19 19:57", "20:30", "2017/08/19 20:30")]
		public void When_parsing_time(DateTime current, string entered, DateTime expected)
		{
			var parser = new TimeParser(() => current);
			var target = parser.Parse(entered);

			target.ShouldBe(expected);
		}
	}
}
