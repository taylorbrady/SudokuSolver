using NUnit.Framework;
using sudokusolver.Solver;

namespace sudokusolver.tests
{
    [TestFixture]
    public class FrozenCellSkippingGridEnumeratorTests
    {
        [Test]
        public void IterateToFirstCellWhenNotFrozen()
        {
            var grid = Grid.OfSize(3);
            grid.Cell(0, 0, 1);
            grid.Cell(0, 1, 2);
            var enumerator = new FrozenCellSkippingGridEnumerator(grid.GetEnumerator());
            enumerator.MoveNext();
            Assert.That(enumerator.Current.IsFrozen, Is.False);
            Assert.That(enumerator.Current.Value, Is.EqualTo(1));
        }

        [Test]
        public void IterateToSecondCellWhenFrozen()
        {
            var grid = Grid.OfSize(3);
            grid.Cell(0, 0, 1);
            grid.Freeze();

            grid.Cell(0, 1, 2);
            var enumerator = new FrozenCellSkippingGridEnumerator(grid.GetEnumerator());
            enumerator.MoveNext();
            Assert.That(enumerator.Current.IsFrozen, Is.False);
            Assert.That(enumerator.Current.Value, Is.EqualTo(2));

        }

        [Test]
        public void CannotIterateBackWhenFirstCellIsFrozen()
        {
            var grid = Grid.OfSize(3);
            grid.Cell(0, 0, 1);
            grid.Freeze();

            grid.Cell(0, 1, 2);
            var enumerator = new FrozenCellSkippingGridEnumerator(grid.GetEnumerator());
            enumerator.MoveNext();
            Assert.That(enumerator.MoveBack, Is.False);

        }

        [Test]
        public void CannotMoveForwardWhenLastCellIsFrozen()
        {
            var grid = Grid.OfSize(3);
            grid.Cell(2, 2, 1);
            grid.Freeze();
            var enumerator = new FrozenCellSkippingGridEnumerator(grid.GetEnumerator());
            int counter = 0;
            while (counter < 8 && enumerator.MoveNext()) counter++;
            Assert.That(counter, Is.EqualTo(8));
            Assert.That(enumerator.MoveNext(), Is.False);
        }

    }
}