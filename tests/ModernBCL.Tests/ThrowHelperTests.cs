using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModernBCL.Tests
{
    // These tests ensure that the ThrowHelper methods, which rely on the 
    // CallerArgumentExpressionAttribute polyfill, function correctly by 
    // throwing the right exception type and capturing the parameter name 
    // automatically.
    [TestClass]
    public class ThrowHelperTests
    {
        // --- ThrowIfNull Tests ---

        [TestMethod]
        public void ThrowIfNull_Throws_ArgumentNullException_WithCorrectParamName()
        {
            object nullArgument = null;

            // The CallerArgumentExpression polyfill should automatically capture "nullArgument"
            var ex = Assert.ThrowsException<ArgumentNullException>(() => ThrowHelper.ThrowIfNull(nullArgument));

            // Assert that the parameter name was correctly captured by the polyfill
            Assert.AreEqual("nullArgument", ex.ParamName, "The ParamName should match the variable name 'nullArgument'.");
        }

        [TestMethod]
        public void ThrowIfNull_DoesNotThrow_WhenNotNull()
        {
            object notNullArgument = new object();

            // This call should execute without throwing an exception
            ThrowHelper.ThrowIfNull(notNullArgument);
        }

        // --- ThrowIfNullOrEmpty Tests ---

        [TestMethod]
        public void ThrowIfNullOrEmpty_Throws_ArgumentNullException_ForNull()
        {
            string nullString = null;
            var ex = Assert.ThrowsException<ArgumentNullException>(() => ThrowHelper.ThrowIfNullOrEmpty(nullString));
            Assert.AreEqual("nullString", ex.ParamName, "Expected ArgumentNullException for null input.");
        }

        [TestMethod]
        public void ThrowIfNullOrEmpty_Throws_ArgumentException_ForEmpty()
        {
            string emptyString = "";
            var ex = Assert.ThrowsException<ArgumentException>(() => ThrowHelper.ThrowIfNullOrEmpty(emptyString));
            Assert.AreEqual("emptyString", ex.ParamName, "Expected ArgumentException for empty string.");
            Assert.IsTrue(ex.Message.Contains("cannot be empty"), "The message should indicate the issue is emptiness.");
        }

        [TestMethod]
        public void ThrowIfNullOrEmpty_DoesNotThrow_WhenValid()
        {
            ThrowHelper.ThrowIfNullOrEmpty("a");
        }

        // --- ThrowIfNullOrWhiteSpace Tests ---

        [TestMethod]
        public void ThrowIfNullOrWhiteSpace_Throws_ArgumentNullException_ForNull()
        {
            string nullString = null;
            var ex = Assert.ThrowsException<ArgumentNullException>(() => ThrowHelper.ThrowIfNullOrWhiteSpace(nullString));
            Assert.AreEqual("nullString", ex.ParamName, "Expected ArgumentNullException for null input.");
        }

        [TestMethod]
        public void ThrowIfNullOrWhiteSpace_Throws_ArgumentException_ForWhitespace()
        {
            string whitespace = "   \t\n";
            var ex = Assert.ThrowsException<ArgumentException>(() => ThrowHelper.ThrowIfNullOrWhiteSpace(whitespace));
            Assert.AreEqual("whitespace", ex.ParamName, "Expected ArgumentException for whitespace string.");
            Assert.IsTrue(ex.Message.Contains("white-space characters"), "The message should indicate the issue is whitespace.");
        }

        [TestMethod]
        public void ThrowIfNullOrWhiteSpace_DoesNotThrow_WhenValid()
        {
            ThrowHelper.ThrowIfNullOrWhiteSpace("valid content");
        }
    }
}