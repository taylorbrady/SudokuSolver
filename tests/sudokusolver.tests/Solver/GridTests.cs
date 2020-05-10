using System;
using NUnit.Framework;
using sudokusolver.Solver;

namespace sudokusolver.tests
{
    [TestFixture]
    public class GridTests
    {

        [Test]
        public void CanCreateGridOfSize9()
        {
            var grid = Grid.OfSize(9);
            Assert.That(grid.Size, Is.EqualTo(9));
        }

        [Test]
        public void WhenSizeNotMultipleOf3_Throw()
        {
            Assert.Throws(typeof(ArgumentException), () => Grid.OfSize(7));
        }

        [Test]
        public void CellDefaultsToZero()
        {
            var grid = Grid.OfSize(3);
            Assert.That(grid.Cell(1,1).Value, Is.EqualTo(0));
        }

        [Test]
        public void CanSetCellValue()
        {
            var grid = Grid.OfSize(3);
            grid.Cell(1, 1, 9);
            Assert.That(grid.Cell(1,1).Value, Is.EqualTo(9));
        }

        [Test]
        public void CanFreezeACell()
        {
            var cell = new Grid.GridCell(0,0);
            cell.Freeze();
            Assert.That(cell.IsFrozen, Is.True);
        }


        [Test]
        public void CannotChangeAFrozenCell()
        {
            var cell = new Grid.GridCell(0,0);
            cell.Freeze();
            Assert.Throws(typeof(InvalidOperationException), () =>
            {
                cell.Value = 9;
            });
        }

        [Test]
        public void WhenGridIsFrozenCellsWithValuesAreFrozen()
        {
            var grid = Grid.OfSize(3);
            grid.Cell(0, 1, 1);
            Assert.That(grid.Cell(0,1).IsFrozen, Is.False);

            grid.Freeze();
            Assert.That(grid.Cell(0,1).IsFrozen, Is.True);

        }

        [Test]
        public void WhenGridIsFrozenCellsWithoutValuesAreNotFrozen()
        {
            var grid = Grid.OfSize(3);
            Assert.That(grid.Cell(0,1).IsFrozen, Is.False);

            grid.Freeze();
            Assert.That(grid.Cell(0,1).IsFrozen, Is.False);

        }

        [Test]
        public void CanLoadGrid()
        {
            var seedValues = new int[][]
            {
                new int[] {1, 0, 3}, new int[] {0, 4, 0}, new int[] {6, 7, 8}
            };

            var grid = Grid.From(seedValues);
            var enumerator = grid.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var currentCell = enumerator.Current;
                Assert.That(currentCell.Value, Is.EqualTo(seedValues[currentCell.Row][currentCell.Column]));
            }

            Console.WriteLine(grid.ToString());
        }


    }
}