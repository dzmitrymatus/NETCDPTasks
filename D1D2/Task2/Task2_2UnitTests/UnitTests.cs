using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Task2_2Library;

namespace Task2_2UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void CheckNullValue()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Task2_2Class.ParseStringToInt(null));
        }

        [TestMethod]
        public void CheckEmptyValue()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Task2_2Class.ParseStringToInt(""));
        }

        [DataTestMethod]
        [DataRow("21474836.6")]
        [DataRow("-21474,21")]
        [DataRow("Hello world")]
        public void CheckBadFormat(string input)
        {
            Assert.ThrowsException<FormatException>(() => Task2_2Class.ParseStringToInt(input));
        }

        [DataTestMethod]
        [DataRow("2147483648")]
        [DataRow("-2147483649")]
        public void CheckOverflowException(string input)
        {
            Assert.ThrowsException<OverflowException>(() => Task2_2Class.ParseStringToInt(input));
        }

        [DataTestMethod]
        [DataRow(int.MinValue)]
        [DataRow(int.MaxValue)]
        [DataRow(0)]
        public void CheckValues(int input)
        {
            var actualResult = Task2_2Class.ParseStringToInt(input.ToString());
            Assert.AreEqual(input, actualResult);
        }
    }
}
