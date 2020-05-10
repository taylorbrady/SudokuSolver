using NUnit.Framework;
using sudokusolver.Solver;

namespace sudokusolver.tests
{
    [TestFixture]
    public class CellSetStateTests
    {
        private Grid _grid;
        private CellSet _set;

        [SetUp]
        public void Setup()
        {
            _grid = Grid.OfSize(3);
            _set = CellSet.FromRow(_grid, 0);
        }

        [Test]
        public void AnEmptyCellSetShouldResolveToPartial()
        {
            Assert.That(_set.State, Is.EqualTo(CellSet.StateValue.Partial));
        }

        [Test]
        public void DuplicatedCellsShouldResolveToInvalid()
        {
            _grid.Cell(0, 0, 1);
            _grid.Cell(0, 1, 1);
            Assert.That(_set.State, Is.EqualTo(CellSet.StateValue.Invalid));

        }

        [Test]
        public void SomeNonEmptyCellsShouldResolveToPartial()
        {

            _grid.Cell(0, 0, 1);

            Assert.That(_set.State, Is.EqualTo(CellSet.StateValue.Partial));
        }

        [Test]
        public void AllUniqueNonEmptyCellsShouldResolveToComplete()
        {
            _grid.Cell(0, 0, 1);
            _grid.Cell(0, 1, 2);
            _grid.Cell(0, 2, 3);
            Assert.That(_set.State, Is.EqualTo(CellSet.StateValue.Complete));

        }

        




    }
}