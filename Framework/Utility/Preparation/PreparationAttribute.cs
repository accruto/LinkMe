using System;

namespace LinkMe.Framework.Utility.Preparation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class InstancePreparationAttribute
        : Attribute
    {
        public abstract void PrepareValue(object value);
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class PreparationAttribute
        : Attribute
    {
        public abstract bool GetValue(object currentValue, out object value);
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PrepareAttribute
        : Attribute
    {
    }
}