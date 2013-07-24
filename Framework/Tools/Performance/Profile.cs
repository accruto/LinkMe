namespace LinkMe.Framework.Tools.Performance
{
    public class Profile
    {
        private readonly string _name;
        private readonly bool _enabled;
        private readonly int _iterations;
        private readonly int _percentage;
        private readonly string _testFixture;
        private readonly Step[] _steps;

        public Profile(string name, bool enabled, int iterations, int percentage, string testFixture, Step[] steps)
        {
            _name = name;
            _testFixture = testFixture;
            _percentage = percentage;
            _iterations = iterations;
            _enabled = enabled;
            _steps = steps;
        }

        public string Name
        {
            get { return _name; }
        }

        public bool Enabled
        {
            get { return _enabled; }
        }

        public int Iterations
        {
            get { return _iterations; }
        }

        public bool Runnable
        {
            get
            {
                // Runnable if the profile is enabled and at least one of the steps is enabled.

                if (!Enabled)
                    return false;

                foreach (var step in Steps)
                {
                    if (step.Enabled)
                        return true;
                }

                return false;
            }
        }

        public int Percentage
        {
            get { return _percentage; }
        }

        public string TestFixture
        {
            get { return _testFixture; }
        }

        public Step[] Steps
        {
            get { return _steps; }
        }
    }
}