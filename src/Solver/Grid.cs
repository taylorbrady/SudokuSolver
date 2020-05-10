using System;
using System.Linq;
using System.Text;
using Ardalis.GuardClauses;

namespace sudokusolver.Solver
{
    public partial class Grid
    {
        private readonly GridCell[][] _cells;

        private Grid(int size)
        {
            this.Size = size;
            this._cells = new GridCell[size][];
            for (int rowIndex = 0; rowIndex < this.Size; rowIndex++)
            {
                var row = new GridCell[size];
                for (int cellIndex = 0; cellIndex < size; cellIndex++) row[cellIndex] = new GridCell(rowIndex, cellIndex);

                this._cells[rowIndex] = row;
            }
        }

        public int Size { get; }

        public IGridCell Cell(int row, int column)
        {
            Guard.Against.OutOfRange(row, nameof(row), 0, Size);
            Guard.Against.OutOfRange(column, nameof(column), 0, Size);
            return this._cells[row][column];
        }

        public void Cell(int row, int column, int value)
        {
            Guard.Against.OutOfRange(row, nameof(row), 0, Size);
            Guard.Against.OutOfRange(column, nameof(column), 0, Size);
            Guard.Against.OutOfRange(value, nameof(value), 0, 9);
            this._cells[row][column].Value = value;
        }

        public void Freeze()
        {
            for (int rowIndex = 0; rowIndex < this.Size; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.Size; colIndex++)
                {
                    var cell = this._cells[rowIndex][colIndex];
                    if (cell.HasValue) cell.Freeze();
                }
            }
        }

        public GridEnumerator GetEnumerator() => new GridEnumerator(this);

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var rowIndex = 0; rowIndex < this.Size; rowIndex++)
            {
                sb.AppendJoin("", this._cells[rowIndex].Select(c => c.Value));
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public static Grid OfSize(int size)
        {
            Guard.Against.NegativeOrZero(size, nameof(size));
            if (size % 3 != 0)
            {
                throw new ArgumentException("the grid size must be in multiples of 3", nameof(size));
            }

            return new Grid(size);
        }

        public static Grid From(int[][] seedValues, bool freeze = true)
        {
            Guard.Against.Null(seedValues, nameof(seedValues));

            var rowCount = seedValues.Length;

            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var columnCount = seedValues[rowIndex].Length;
                if (rowCount != columnCount) throw new ArgumentException($"a square matrix must be provided where the number of rows matches the number of columns. Row {rowIndex} contains {columnCount} elements");
            }

            var grid = Grid.OfSize(rowCount);
            var enumerator = grid.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var currentCell = enumerator.Current;
                currentCell.Value = seedValues[currentCell.Row][currentCell.Column];
            }

            if (freeze) grid.Freeze();

            return grid;
        }

        public class GridCell : IGridCell
        {
            private int _value = 0;

            public int Row { get; }
            public int Column { get; }

            public GridCell(int row, int column)
            {
                Guard.Against.Negative(row, nameof(row));
                Guard.Against.Negative(column, nameof(column));
                Row = row;
                Column = column;
            }

            public int Value
            {
                get => this._value;
                set
                {
                    Guard.Against.OutOfRange(value, nameof(value), 0, 9);

                    if (this.IsFrozen) throw new InvalidOperationException("the cell is frozen and cannot be modified");
                    
                    this._value = value;
                }
            }

            public bool IsFrozen { get; private set; } = false;

            public bool HasValue => this.Value > 0;
            public void Restart()
            {
                this.Value = 0;
            }

            public void Freeze()
            {
                this.IsFrozen = true;
            }

        }

        public class GridEnumerator : IGridEnumerator
        {
            private int _row = 0;
            private int _column = -1;

            public Grid Grid { get; }

            public GridEnumerator(Grid grid)
            {
                Guard.Against.Null(grid, nameof(grid));
                Grid = grid;
            }

            private void AssertValidPosition()
            {
                if (this._column < 0) 
                    throw new InvalidOperationException("must call MoveNext() before the first usage of Current or MoveBack()");
            }

            public IGridCell Current
            {
                get
                {
                    AssertValidPosition();
                    return this.Grid.Cell(this._row, this._column);
                }
            }

            public bool MoveNext()
            {
                var row = this._row;
                var col = this._column;
                col++;
                if (col >= this.Grid.Size)
                {
                    col = 0;
                    row++;
                }

                if (row >= this.Grid.Size) return false;

                this._row = row;
                this._column = col;
                return true;
            }

            public bool MoveBack()
            {
                AssertValidPosition();

                var row = this._row;
                var col = this._column;
                col--;
                if (col < 0)
                {
                    col = this.Grid.Size - 1;
                    row--;
                }

                if (row < 0) return false;

                this._row = row;
                this._column = col;
                return true;
            }

            public void Reset()
            {
                this._row = 0;
                this._column = -1;
            }
        }
    }
}