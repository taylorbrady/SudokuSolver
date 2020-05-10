using System.Runtime.InteropServices.ComTypes;

namespace sudokusolver.Solver
{
    public class FrozenCellSkippingGridEnumerator : IGridEnumerator
    {
        private readonly IGridEnumerator _wrappedEnumerator;

        public FrozenCellSkippingGridEnumerator(IGridEnumerator wrappedEnumerator)
        {
            _wrappedEnumerator = wrappedEnumerator;
        }


        public Grid Grid => _wrappedEnumerator.Grid;

        public Grid.IGridCell Current => _wrappedEnumerator.Current;

        public bool MoveNext()
        {
            var result = _wrappedEnumerator.MoveNext();
            while (result && _wrappedEnumerator.Current.IsFrozen)
            {
                result = _wrappedEnumerator.MoveNext();
            }

            return result;
        }

        public bool MoveBack()
        {
            var result = _wrappedEnumerator.MoveBack();
            while (result && _wrappedEnumerator.Current.IsFrozen)
            {
                result = _wrappedEnumerator.MoveBack();
            }

            return result;
        }

        public void Reset()
        {
            _wrappedEnumerator.Reset();
        }
    }
}