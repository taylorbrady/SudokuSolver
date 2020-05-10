using System;
using NUnit.Framework;
using sudokusolver.Solver;

namespace sudokusolver.tests
{
    [TestFixture]
    public class CellSetTests
    {

        [Test]
        public void CanCreateCellSetFromRow()
        {
            var grid = Grid.OfSize(3);
            var cellSet = CellSet.FromRow(grid, 0);

            Assert.That(cellSet.CellReferences.Count, Is.EqualTo(3));
            Assert.That(cellSet.CellReferences.Contains(new Tuple<int, int>(0,0)));
            Assert.That(cellSet.CellReferences.Contains(new Tuple<int, int>(0,1)));
            Assert.That(cellSet.CellReferences.Contains(new Tuple<int, int>(0,2)));
        }

        [Test]
        public void CanCreateCellSetFromCol()
        {
            var grid = Grid.OfSize(3);
            var cellSet = CellSet.FromColumn(grid, 0);

            Assert.That(cellSet.CellReferences.Count, Is.EqualTo(3));
            Assert.That(cellSet.CellReferences.Contains(new Tuple<int, int>(0,0)));
            Assert.That(cellSet.CellReferences.Contains(new Tuple<int, int>(1,0)));
            Assert.That(cellSet.CellReferences.Contains(new Tuple<int, int>(2,0)));
        }

        [Test]
        public void CanCreateCellSetFromSubgrid()
        {
            var grid = Grid.OfSize(3);
            var cellSet = CellSet.FromSubGrid(grid, 0, 0);

            Assert.That(cellSet.CellReferences.Count, Is.EqualTo(9));
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Assert.That(cellSet.CellReferences.Contains(new Tuple<int, int>(row,col)));
                    
                }
            }
        }

    }
}