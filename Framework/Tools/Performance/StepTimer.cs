using System.Diagnostics;

namespace LinkMe.Framework.Tools.Performance
{
    public class StepTimer
    {
        private Stopwatch _stopwatch;

        internal StepTimer()
		{
        }

        public void Start()
        {
            _stopwatch = Stopwatch.StartNew();
		}

        public void Stop()
        {
            // Could perhaps make some checks to make sure stops are matched to starts etc.

            _stopwatch.Stop();
        }

        public long ElapsedTicks
        {
            get { return _stopwatch.ElapsedTicks; }
        }

        public long ElapsedMilliseconds
        {
            get { return _stopwatch.ElapsedMilliseconds; }
        }
    }
}
