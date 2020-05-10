using System;
using NUnit.Framework;
using sudokusolver.Solver;

namespace sudokusolver.tests
{
    [TestFixture]
    public class GridEnumeratorTests
    {

        [Test]
        public void WhenUsingCurrentBeforeMoveNext_Throw()
        {
            var grid = Grid.OfSize(3);
            var enumerator = grid.GetEnumerator();
            Assert.Throws<InvalidOperationException>(() =>
            {
                var x = enumerator.Current;
            });
        }

        [Test]
        public void EnumeratorReturnsValue()
        {
            var grid = Grid.OfSize(3);
            grid.Cell(0, 0, 7);
            var enumerator = grid.GetEnumerator();
            Assert.That(enumerator.MoveNext(), Is.True);
            Assert.That(enumerator.Current.Value, Is.EqualTo(7));
        }

        [Test]
        public void MovingForwardOffTheGridReturnsFalse()
        {
            var grid = Grid.OfSize(3);
            var enumerator = grid.GetEnumerator();
            for (int move = 0; move < 3*3; move++)
            {
                Assert.That(enumerator.MoveNext(), Is.True);
            }
            Assert.That(enumerator.MoveNext(), Is.False);

        }

        [Test]
        public void WhenUsingMoveBackBeforeMoveNext_Throw()
        {
            var grid = Grid.OfSize(3);
            var enumerator = grid.GetEnumerator();
            Assert.Throws<InvalidOperationException>(() =>
            {
                var x = enumerator.MoveBack();
            });
        }

        [Test]
        public void MovingBackwardsOffTheGridReturnsFalse()
        {
            var grid = Grid.OfSize(3);
            var enumerator = grid.GetEnumerator();
            enumerator.MoveNext();
            Assert.That(enumerator.MoveBack(), Is.False);
        }
    }
}