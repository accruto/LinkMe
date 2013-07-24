using System;
using System.Runtime.InteropServices;

namespace LinkMe.Utility.PerfMon
{
	
	public class HighResTimer
	{
		private static readonly Decimal multiplier = new Decimal(1.0e9);
		private long start;
		private long stop;
		private long frequency;    

		public HighResTimer()
		{
			if (QueryPerformanceFrequency(out frequency) == false)
			{
				throw new ApplicationException("High Res Timer Frequency Not Supported");
			}
		}

		public void Start()
		{
			QueryPerformanceCounter(out start);
		}

		public void Stop()
		{
			QueryPerformanceCounter(out stop);
		}

		/// <summary>
		/// Use this measurement when retrieving actual Milliseconds (ie for tests).
		/// </summary>
		/// <returns></returns>
		public long DurationMilliseconds
		{
			get
			{
				return (long)( Duration() / 1000000);
			}
		}

		public double Duration()
		{
			return Duration(1);
		}

		public double Duration(int iterations)
		{
			return ((((double)(stop - start)* (double) multiplier) / (double) frequency)/iterations);
		}

		/// <summary>
		/// Use this measurement when expecting Milliseconds from Performance Counters! such as AverageTimer32. Will not return actuall ms but instead
		/// a number which is translated to ms by the counters
		/// </summary>
		public long CounterMilliseconds
		{
			get
			{
				return (stop - start ) * 1000;
			}
		}

		[DllImport("KERNEL32")]
		private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceFrequency(out long lpFrequency);

	}
}
