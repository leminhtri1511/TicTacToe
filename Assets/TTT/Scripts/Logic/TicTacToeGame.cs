using TTT.Scripts.Manager;

namespace TTT.Scripts.Logic
{
    public class TicTacToeGame
    {
        public CellState CurrentPlayer { get; private set; }
        public GameState State { get; private set; }
        public WinningLine WinningLine { get; private set; }

        private const int BOARD_SIZE = 3;
        private readonly CellState[,] _board = new CellState[BOARD_SIZE, BOARD_SIZE];

        public void StartGame()
        {
            ClearBoard();

            CurrentPlayer = CellState.X;

            State = GameState.Playing;

            WinningLine = default;
        }

        public bool MakeMove(int row, int column)
        {
            if (State != GameState.Playing) return false;

            if (!IsValidPosition(row, column)) return false;

            if (_board[row, column] != CellState.Empty) return false;

            _board[row, column] = CurrentPlayer;

            UpdateGameState();

            if (State == GameState.Playing)
            {
                SwitchPlayer();
            }

            return true;
        }

        public CellState GetCell(int row, int column)
        {
            if (!IsValidPosition(row, column)) return CellState.Empty;

            return _board[row, column];
        }

        private void UpdateGameState()
        {
            WinningLine winningLine = FindWinningLine(CurrentPlayer);

            if (winningLine.HasWinner)
            {
                WinningLine = winningLine;
                State = CurrentPlayer == CellState.X ? GameState.XWin : GameState.OWin;
                return;
            }

            if (IsBoardFull())
            {
                State = GameState.Draw;
            }
        }

        private WinningLine FindWinningLine(CellState player)
        {
            for (int row = 0; row < BOARD_SIZE; row++)
            {
                if (_board[row, 0] == player &&
                    _board[row, 1] == player &&
                    _board[row, 2] == player)
                {
                    return new WinningLine(row, 0, row, 2);
                }
            }

            // Columns
            for (int column = 0; column < BOARD_SIZE; column++)
            {
                if (_board[0, column] == player &&
                    _board[1, column] == player &&
                    _board[2, column] == player)
                {
                    return new WinningLine(0, column, 2, column);
                }
            }

            // Diagonal \
            if (_board[0, 0] == player &&
                _board[1, 1] == player &&
                _board[2, 2] == player)
            {
                return new WinningLine(0, 0, 2, 2);
            }

            // Diagonal /
            if (_board[0, 2] == player &&
                _board[1, 1] == player &&
                _board[2, 0] == player)
            {
                return new WinningLine(0, 2, 2, 0);
            }

            return default;
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == CellState.X ? CellState.O : CellState.X;
        }

        private bool IsBoardFull()
        {
            for (int row = 0; row < BOARD_SIZE; row++)
            {
                for (int column = 0; column < BOARD_SIZE; column++)
                {
                    if (_board[row, column] == CellState.Empty) return false;
                }
            }

            return true;
        }

        private void ClearBoard()
        {
            for (int row = 0; row < BOARD_SIZE; row++)
            {
                for (int column = 0; column < BOARD_SIZE; column++)
                {
                    _board[row, column] = CellState.Empty;
                }
            }
        }

        private bool IsValidPosition(int row, int column)
        {
            return row is >= 0 and < BOARD_SIZE && column is >= 0 and < BOARD_SIZE;
        }
    }
}