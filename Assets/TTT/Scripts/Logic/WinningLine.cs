namespace TTT.Scripts.Logic
{
    public struct WinningLine
    {
        public bool HasWinner;

        public int StartRow;
        public int StartColumn;

        public int EndRow;
        public int EndColumn;

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