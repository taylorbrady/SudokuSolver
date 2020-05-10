using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;

namespace sudokusolver.Solver
{
    public class CellSetCollection
    {

        private readonly List<CellSet> _cellSets;

        private CellSetCollection(List<CellSet> cellSets)
        {
            this._cellSets = cellSets;
        }

        public CellSet.StateValue AggregatedState
        {
            get
            {
                var allStates = _cellSets.Select(cs => cs.State);
                if (allStates.Any(s => s == CellSet.StateValue.Invalid)) return CellSet.StateValue.Invalid;
                if (allStates.Any(s => s == CellSet.StateValue.Partial)) return CellSet.StateValue.Partial;
                return CellSet.StateValue.Complete;
            }
        }

        public ICollection<CellSet> CellSets => this._cellSets;

        public static CellSetCollection For(Grid grid)
        {
            Guard.Against.Null(grid, nameof(grid));

            var cellsets = new List<CellSet>();

            var rowsSets = Enumerable.Range(0, grid.Size)
                .Select(r => CellSet.FromRow(grid, r));
            cellsets.AddRange(rowsSets);

            var columnSets = Enumerable.Range(0, grid.Size)
                .Select(c => CellSet.FromColumn(grid, c));
            cellsets.AddRange(columnSets);

            for (int row = 0; row < grid.Size; row += 3)
            {
                for (int col = 0; col < grid.Size; col += 3)
                {
                    cellsets.Add(CellSet.FromSubGrid(grid, row, col));
                }
            }

            return new CellSetCollection(cellsets);
        }

    }
}