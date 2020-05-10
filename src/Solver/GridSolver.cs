using System;

namespace sudokusolver.Solver
{
    public class GridSolver
    {

        private readonly Grid _grid;

        public GridSolver(Grid grid)
        {
            _grid = grid;
        }

        public bool Solve()
        {
            var enumerator = new FrozenCellSkippingGridEnumerator(this._grid.GetEnumerator());
            var cellSets = CellSetCollection.For(this._grid);

            if (!enumerator.MoveNext()) return false; // can't even start

            while (true)
            {
                var newValue = enumerator.Current.Value + 1;

                while (newValue < 10)
                {
                    enumerator.Current.Value = newValue;
                    if (cellSets.AggregatedState != CellSet.StateValue.Invalid) break;
                    newValue++;
                }

                switch (cellSets.AggregatedState)
                {
                    case CellSet.StateValue.Invalid:
                    {
                        do
                        {
                            enumerator.Current.Restart();
                            if (!enumerator.MoveBack()) return false;

                        } while (enumerator.Current.Value == 9);
                        break;
                    }
                    case CellSet.StateValue.Complete:
                        return true;
                    case CellSet.StateValue.Partial when !enumerator.MoveNext():
                        return false;
                    
                }
            }

            throw new InvalidOperationException("reached an unexpected grid state");
        }
    }
}