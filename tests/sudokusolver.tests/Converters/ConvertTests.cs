using System;
using NUnit.Framework;
using sudokusolver.Converters;

namespace sudokusolver.tests.Converters
{
    [TestFixture]
    public class ConvertTests
    {

        [Test]
        public void CanSplitStrings()
        {
            var result = Converter.From("02\n13", "\n");
            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result[0][0], Is.EqualTo(0));
            Assert.That(result[0][1], Is.EqualTo(2));
            Assert.That(result[1][0], Is.EqualTo(1));
            Assert.That(result[1][1], Is.EqualTo(3));
        }

        [Test]
        public void NonNumericsThrows()
        {
            Assert.Throws(typeof(FormatException), () =>
            {
                Converter.From("a\nb", "\n");
            });

        }

        [Test]
        public void IfADelimiterIsProvideButNotUsesThrow()
        {
            Assert.Throws(typeof(ArgumentException), () =>
            {
                sudokusolver.Converters.Converter.From("0123", "\n");
            });
        }

        [Test]
        [TestCase("\n")]
        [TestCase("\r\n")]
        public void IfNoDelimiterPresentAutoDetect(string delimiterToFind)
        {
            var body = $"0{delimiterToFind}1";
            var result = Converter.From(body, null);
            Assert.That(result.Length, Is.EqualTo(2));
        }

        [Test]
        public void IfNoDelimiterPresentAndAutoDetectFailsThrow()
        {
            Assert.Throws(typeof(ArgumentException), () =>
            {
                sudokusolver.Converters.Converter.From("0123", null);
            });
        }

    }
}