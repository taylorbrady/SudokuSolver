using System;
using NUnit.Framework;
using sudokusolver.Solver;

namespace sudokusolver.tests
{
    [TestFixture]
    public class GridSolverTests
    {

        [Test]
        public void AnEmptyGridIsSolvedWithOnlyMovingForward()
        {
            var grid = Grid.OfSize(3);
            var solver = new GridSolver(grid);
            Assert.That(solver.Solve(), Is.True); 
            Console.WriteLine(grid.ToString());
        }

        [Test]
        public void ForceASingleValue()
        {
            var grid = Grid.OfSize(3);
            grid.Cell(2, 1, 5);
            grid.Freeze();
            var solver = new GridSolver(grid);
            Assert.That(solver.Solve(), Is.True); 
            Console.WriteLine(grid.ToString());
        }

        [Test]
        public void AnUnsolvableGrid()
        {
            var grid = Grid.OfSize(3);
            grid.Cell(1, 1, 5);
            grid.Cell(2, 1, 5);
            grid.Freeze();
            var solver = new GridSolver(grid);
            Assert.That(solver.Solve(), Is.False); 
            Console.WriteLine(grid.ToString());
        }

        [Test]
        public void SolveAFullPuzzle()
        {
            var values = new int[][]
            {
                new int[] {0, 9, 0, 0, 0, 8, 6, 0, 4},
                new int[] {6, 0, 0, 0, 1, 9, 0, 0, 0},
                new int[] {0, 4, 8, 6, 2, 5, 0, 0, 0},
                new int[] {0, 2, 0, 0, 8, 0, 7, 1, 6},
                new int[] {0, 7, 0, 0, 0, 0, 0, 8, 0},
                new int[] {3, 8, 5, 0, 7, 0, 0, 2, 0},
                new int[] {0, 0, 0, 8, 6, 1, 5, 4, 0},
                new int[] {0, 0, 0, 3, 5, 0, 0, 0, 2},
                new int[] {5, 0, 7, 2, 0, 0, 0, 3, 0},
            };

            var grid = Grid.From(values);
            var solver = new GridSolver(grid);
            Assert.That(solver.Solve(), Is.True);
            Console.WriteLine(grid.ToString());

        }

    }
}