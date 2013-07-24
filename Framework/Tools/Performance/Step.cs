using System;
using System.Collections;
using System.Collections.Generic;
using IEnumerator=System.Collections.IEnumerator;

namespace LinkMe.Framework.Tools.Performance
{
    public abstract class RandomValue
    {
        private readonly int _min;
        private readonly int _max;
        private readonly Random _random = new Random();

        protected RandomValue(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public int Next
        {
            get { return _min == _max ? _min : _random.Next(_min, _max); }
        }

        public virtual int Min
        {
            get { return _min; }
        }

        public virtual int Max
        {
            get { return _max; }
        }
    }

    public class Delay
        : RandomValue
    {
        public Delay(int min, int max)
            : base(min * 1000, max * 1000)
        {
            // Store everything as milliseconds.
        }

        public override int Min
        {
            get { return base.Min / 1000; }
        }

        public override int Max
        {
            get { return base.Max / 1000; }
        }
    }

    public class Iterations
        : RandomValue
    {
        public Iterations(int min, int max)
            : base(min, max + 1)
        {
            // Want the randomness to be inclusive of the maximum.
        }

        public override int Max
        {
            get { return base.Max - 1; }
        }
    }

    public abstract class Step
    {
        private readonly bool _enabled;
        private readonly int _percentage;
        private readonly Iterations _iterations;

        protected Step(bool enabled, int percentage, int minIterations, int maxIterations)
        {
            _enabled = enabled;
            _percentage = percentage;
            _iterations = new Iterations(minIterations, maxIterations);
        }

        public bool Enabled
        {
            get { return _enabled; }
        }

        public int Percentage
        {
            get { return _percentage; }
        }

        public Iterations Iterations
        {
            get { return _iterations; }
        }
    }

    public class ComponentStep
        : Step
    {
        private readonly string _name;
        private readonly Delay _delay;

        public ComponentStep(string name, bool enabled, int percentage, int minIterations, int maxIterations, int minDelay, int maxDelay)
            : base(enabled, percentage, minIterations, maxIterations)
        {
            _name = name;
            _delay = new Delay(minDelay, maxDelay);
        }

        public string Name
        {
            get { return _name; }
        }

        public Delay Delay
        {
            get { return _delay; }
        }
    }

    public abstract class StepList
        : Step, IEnumerable<Step>
    {
        private readonly Step[] _childSteps = new Step[0];

        protected StepList()
            : base(true, 100, 1, 1)
        {
        }

        IEnumerator<Step> IEnumerable<Step>.GetEnumerator()
        {
            return ((IEnumerable<Step>)_childSteps).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_childSteps).GetEnumerator();
        }
    }

    public class OrStep
        : StepList
    {
    }

    public class AndStep
        : StepList
    {
    }
}