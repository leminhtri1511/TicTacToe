using System.Collections.Generic;
using TTT.Scripts.Utils;

namespace TTT.Scripts.Logic
{
    public class TicTacToeGame
    {
        public CellIdentity CurrentPlayer { get; private set; }
        public GameState GameState { get; private set; }

        private static int BoardSize => TTTUtils.BOARD_SIZE;
        private readonly CellIdentity[,] _board = new CellIdentity[BoardSize, BoardSize];
        private readonly CellPosition[] _winningCells = new CellPosition[3];

        public IReadOnlyList<CellPosition> WinningCells => _winningCells;

        public bool HasWinningCells => GameState is GameState.XWin or GameState.OWin;

        public void StartGame()
        {
            ClearBoard();

            CurrentPlayer = CellIdentity.X;
            GameState = GameState.Playing;
        }

        public bool MakeMove(int row, int column)
        {
            if (GameState != GameState.Playing) return false;

            if (!IsValidPosition(row, column)) return false;

            if (_board[row, column] != CellIdentity.Empty) return false;

            _board[row, column] = CurrentPlayer;

            UpdateGameState();

            if (GameState == GameState.Playing)
            {
                SwitchPlayer();
            }

            return true;
        }

        public CellIdentity GetCell(int row, int column)
        {
            if (!IsValidPosition(row, column)) return CellIdentity.Empty;

            return _board[row, column];
        }

        private void UpdateGameState()
        {
            if (TryFindWinner(CurrentPlayer))
            {
                GameState = CurrentPlayer == CellIdentity.X ? GameState.XWin : GameState.OWin;
                return;
            }

            if (IsBoardFull())
            {
                GameState = GameState.Draw;
            }
        }

        private bool TryFindWinner(CellIdentity player)
        {
            // Rows
            for (int row = 0; row < BoardSize; row++)
            {
                if (_board[row, 0] == player &&
                    _board[row, 1] == player &&
                    _board[row, 2] == player)
                {
                    SetWinningCells(row, 0, row, 1, row, 2);
                    return true;
                }
            }

            // Columns
            for (int column = 0; column < BoardSize; column++)
            {
                if (_board[0, column] == player &&
                    _board[1, column] == player &&
                    _board[2, column] == player)
                {
                    SetWinningCells(0, column, 1, column, 2, column);
                    return true;
                }
            }

            // Diagonal \
            if (_board[0, 0] == player &&
                _board[1, 1] == player &&
                _board[2, 2] == player)
            {
                SetWinningCells(0, 0, 1, 1, 2, 2);

                return true;
            }

            // Diagonal /
            if (_board[0, 2] == player &&
                _board[1, 1] == player &&
                _board[2, 0] == player)
            {
                SetWinningCells(0, 2, 1, 1, 2, 0);
                return true;
            }

            return false;
        }

        private void SetWinningCells(int row1, int column1, int row2, int column2, int row3, int column3)
        {
            _winningCells[0] = new CellPosition(row1, column1);
            _winningCells[1] = new CellPosition(row2, column2);
            _winningCells[2] = new CellPosition(row3, column3);
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == CellIdentity.X ? CellIdentity.O : CellIdentity.X;
        }

        private bool IsBoardFull()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int column = 0; column < BoardSize; column++)
                {
                    if (_board[row, column] == CellIdentity.Empty) return false;
                }
            }

            return true;
        }

        private void ClearBoard()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int column = 0; column < BoardSize; column++)
                {
                    _board[row, column] = CellIdentity.Empty;
                }
            }

            ClearWinningCells();
        }

        private void ClearWinningCells()
        {
            for (int i = 0; i < _winningCells.Length; i++)
            {
                _winningCells[i] = default;
            }
        }

        private bool IsValidPosition(int row, int column)
        {
            return row is >= 0 and < TTTUtils.BOARD_SIZE && column is >= 0 and < TTTUtils.BOARD_SIZE;
        }
    }
}