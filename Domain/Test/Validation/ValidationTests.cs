using System;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Validation
{
    public class DataObject
    {
        [Required]
        public string RequiredValue { get; set; }
        [StringLength(6, 12)]
        public string LengthValue { get; set; }
    }

    [TestClass]
    public class ValidationTests
    {
        [TestMethod, ExpectedException(typeof(ValidationErrorsException))]
        public void TestRequired()
        {
            var o = new DataObject();
            o.Validate();
        }

        [TestMethod]
        public void TestLogException()
        {
            var eventSource = new EventSource<ValidationTests>();

            try
            {
                var o = new DataObject { LengthValue = new string('a', 5) };
                o.Validate();
            }
            catch (Exception ex)
            {
                eventSource.Raise(Event.Error, "method", ex, null);
            }
        }

        [TestMethod]
        public void TestLogExceptionAsArg()
        {
            var eventSource = new EventSource<ValidationTests>();

            try
            {
                var o = new DataObject { LengthValue = new string('a', 5) };
                o.Validate();
            }
            catch (Exception ex)
            {
                eventSource.Raise(Event.Error, "method", Event.Arg("exception", ex));
            }
        }

        [TestMethod]
        public void TestLogErrorsAsArg()
        {
            var eventSource = new EventSource<ValidationTests>();

            try
            {
                var o = new DataObject { LengthValue = new string('a', 5) };
                o.Validate();
            }
            catch (ValidationErrorsException ex)
            {
                eventSource.Raise(Event.Error, "method", Event.Arg("error0", ex.Errors[0]), Event.Arg("error1", ex.Errors[1]));
            }
        }
    }
}
