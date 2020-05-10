using System;
using System.Linq;
using NUnit.Framework;
using sudokusolver.Solver;

namespace sudokusolver.tests
{
    [TestFixture]
    public class CellSetCollectionTests
    {

        [Test]
        public void A3x3GridShouldHave3Rows()
        {
            var grid = Grid.OfSize(3);
            var setCollection = CellSetCollection.For(grid);
            Assert.That(setCollection.CellSets.Any(cs => "row 0".Equals(cs.Name)), Is.True);
            Assert.That(setCollection.CellSets.Any(cs => "row 1".Equals(cs.Name)), Is.True);
            Assert.That(setCollection.CellSets.Any(cs => "row 2".Equals(cs.Name)), Is.True);
        }

        [Test]
        public void A3x3GridShouldHave3Columns()
        {
            var grid = Grid.OfSize(3);
            var setCollection = CellSetCollection.For(grid);
            Assert.That(setCollection.CellSets.Any(cs => "column 0".Equals(cs.Name)), Is.True);
            Assert.That(setCollection.CellSets.Any(cs => "column 1".Equals(cs.Name)), Is.True);
            Assert.That(setCollection.CellSets.Any(cs => "column 2".Equals(cs.Name)), Is.True);
        }

        [Test]
        public void A3x3GridShouldHave1SubGrid()
        {
            var grid = Grid.OfSize(3);
            var setCollection = CellSetCollection.For(grid);
            Assert.That(setCollection.CellSets.Any(cs => "subgrid [0,0]".Equals(cs.Name)), Is.True);
        }

        [Test]
        public void A6x6GridShouldHave4SubGrid()
        {
            var grid = Grid.OfSize(6);
            var setCollection = CellSetCollection.For(grid);
            Assert.That(setCollection.CellSets.Any(cs => "subgrid [0,0]".Equals(cs.Name)), Is.True);
            Assert.That(setCollection.CellSets.Any(cs => "subgrid [0,3]".Equals(cs.Name)), Is.True);
            Assert.That(setCollection.CellSets.Any(cs => "subgrid [3,0]".Equals(cs.Name)), Is.True);
            Assert.That(setCollection.CellSets.Any(cs => "subgrid [3,3]".Equals(cs.Name)), Is.True);
        }

        [Test]
        public void AnEmptyGridShouldHaveAStateOfPartial()
        {
            var grid = Grid.OfSize(3);
            var setCollection = CellSetCollection.For(grid);
            Assert.That(setCollection.AggregatedState, Is.EqualTo(CellSet.StateValue.Partial));
        }

        [Test]
        public void SomeEmptyCellsShouldHaveAStateOfInvalid()
        {
            var grid = Grid.OfSize(3);
            grid.Cell(0, 0, 1);
            var setCollection = CellSetCollection.For(grid);
            Assert.That(setCollection.AggregatedState, Is.EqualTo(CellSet.StateValue.Partial));
        }


        [Test]
        public void DuplicatedCellsShouldHaveAStateOfInvalid()
        {
            var grid = Grid.OfSize(3);
            grid.Cell(0, 0, 1);
            grid.Cell(0, 1, 1);
            var setCollection = CellSetCollection.For(grid);
            Assert.That(setCollection.AggregatedState, Is.EqualTo(CellSet.StateValue.Invalid));
        }

        [Test]
        public void CompletedGridShouldHaveAStateOfComplete()
        {
            var grid = Grid.OfSize(3);
            for (int index = 0; index < 9; index++)
            {
                grid.Cell(index/3, index%3, index+1);
            }
            var setCollection = CellSetCollection.For(grid);
            Assert.That(setCollection.AggregatedState, Is.EqualTo(CellSet.StateValue.Complete));
        }

    }
}