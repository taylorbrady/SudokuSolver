using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;

namespace sudokusolver.Solver
{
    public class CellSet
    {
        public enum StateValue
        {
            Invalid = 0,
            Partial = 1,
            Complete = 2
        }

        private readonly Grid _grid;

        private readonly List<Tuple<int,int>> _cellRefs = new List<Tuple<int, int>>();

        public string Name { get; private set; }

        public CellSet(string name, Grid grid, IEnumerable<Tuple<int, int>> cellRefs)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.Null(grid, nameof(Grid));
            Guard.Against.Null(cellRefs, nameof(cellRefs));

            if (!cellRefs.Any()) throw new InvalidOperationException("incorrect number of cellrefs was supplied");

            this.Name = name;
            this._grid = grid;
            this._cellRefs.AddRange(cellRefs);
        }

        public StateValue State
        {
            get
            {
                var cellSetValues = _cellRefs
                    .Select(cellref => _grid.Cell(cellref.Item1, cellref.Item2));

                var valueCounts = cellSetValues
                    .Select(c => c.Value)
                    .GroupBy(v => v)
                    .Select(g => new Tuple<int, int>(g.Key, g.Count()))
                    .ToList(); // [value, count]

                var anyDuplicatedNonZeroValues = valueCounts
                    .Where(p => p.Item1 > 0)
                    .Any(p => p.Item2 > 1);
                if (anyDuplicatedNonZeroValues) return StateValue.Invalid;

                var anyUnsetCells = valueCounts.Any(p => p.Item1 == 0);
                if (anyUnsetCells) return StateValue.Partial;

                return StateValue.Complete;

            }

        }

        public ICollection<Tuple<int, int>> CellReferences => this._cellRefs;

        public static CellSet FromRow(Grid grid, int row)
        {
            var cellRefs = 
                Enumerable.Range(0, grid.Size)
                .Select(col => new Tuple<int, int>(row, col));

            return new CellSet($"row {row}", grid, cellRefs);
        }

        public static CellSet FromColumn(Grid grid, int column)
        {
            var cellRefs = 
                Enumerable.Range(0, grid.Size)
                    .Select(row => new Tuple<int, int>(row, column));
            
            return new CellSet($"column {column}", grid, cellRefs);

        }

        public static CellSet FromSubGrid(Grid grid, int topRow, int topLeftColumn)
        {
            var cellRefs = 
                Enumerable.Range(0, 9)
                    .Select(index => new Tuple<int, int>(index / 3, index % 3));

            return new CellSet($"subgrid [{topRow},{topLeftColumn}]", grid, cellRefs);
        }

    }
}