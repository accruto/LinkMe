using System;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test
{
    public static class AssertException
    {
        public static void Thrown<TE>(Action action)
            where TE : Exception
        {
            if (typeof(TE) == typeof(ValidationErrorsException))
                Thrown<TE>(action, null, AssertValidationErrors);
            else
                Thrown<TE>(action, null, AssertThrownException);
        }

        public static void Thrown<TE>(Action action, string message)
            where TE : Exception
        {
            if (typeof(TE) == typeof(ValidationErrorsException))
                Thrown<TE>(action, message, AssertValidationErrors);
            else
                Thrown<TE>(action, message, AssertThrownException);
        }

        private static void Thrown<TE>(Action action, string message, Action<Exception, string> assert)
        {
            try
            {
                action();
                Assert.Fail("Expected an exception of type '" + typeof(TE) + "' to be thrown but none was.");
            }
            catch (AssertFailedException)
            {
                // Don't process the exception from the Assert.Fail here.

                throw;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TE));
                assert(ex, message);
            }
        }

        private static void AssertThrownException(Exception ex, string message)
        {
            if (message != null)
                Assert.AreEqual(message, ex.Message);
        }

        private static void AssertValidationErrors(Exception ex, string message)
        {
            var exception = (ValidationErrorsException) ex;
            Assert.AreEqual(1, exception.Errors.Count);
            if (message != null)
                Assert.AreEqual(message, exception.Errors[0].Message);
        }
    }
}
