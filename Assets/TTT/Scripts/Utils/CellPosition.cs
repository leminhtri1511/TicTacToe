namespace TTT.Scripts.Utils
{
    public readonly struct CellPosition
    {
        public int Row { get; }
        public int Column { get; }

        public CellPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}