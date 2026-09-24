namespace TTT.Scripts.Logic
{
    public struct WinningLine
    {
        public readonly bool HasWinner;

        public readonly int StartRow;
        public readonly int StartColumn;

        public readonly int EndRow;
        public readonly int EndColumn;

        public WinningLine(int startRow, int startColumn, int endRow, int endColumn)
        {
            HasWinner = true;

            StartRow = startRow;
            StartColumn = startColumn;

            EndRow = endRow;
            EndColumn = endColumn;
        }
    }
}