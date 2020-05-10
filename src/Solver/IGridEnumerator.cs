namespace sudokusolver.Solver
{
    public interface IGridEnumerator
    {
        Grid Grid { get; }
        Grid.IGridCell Current { get; }
        bool MoveNext();
        bool MoveBack();
        void Reset();
    }
}