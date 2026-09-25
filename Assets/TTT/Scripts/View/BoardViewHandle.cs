using System;
using System.Collections.Generic;
using TTT.Scripts.Utils;
using UnityEngine;

namespace TTT.Scripts.View
{
    public class BoardViewHandle : MonoBehaviour
    {
        [Header("Cells")]
        [SerializeField] private UICell[] _cells;

        private static int BoardSize => TTTUtils.BOARD_SIZE;

        public void InitializeBoard(Action<int, int> onCellClicked)
        {
            if (_cells.Length != BoardSize * BoardSize)
            {
                Debug.LogError($"TicTacToe Board requires {BoardSize * BoardSize} cells.");
                return;
            }

            for (int row = 0; row < BoardSize; row++)
            {
                for (int column = 0; column < BoardSize; column++)
                {
                    var index = GetIndex(row, column);

                    _cells[index].Initialize(row, column, onCellClicked);
                }
            }
        }

        public void UpdateCell(int row, int column, CellIdentity cellIdentity)
        {
            var index = GetIndex(row, column);

            _cells[index].SetCellState(cellIdentity);
        }

        public void ResetBoard()
        {
            foreach (var cell in _cells)
            {
                cell.SetCellState(CellIdentity.Empty);
            }
        }

        public void SetBoardInteractable(bool interactable)
        {
            foreach (var cell in _cells)
            {
                cell.SetInteractable(interactable);
            }
        }

        public void ShowWinningCells(IReadOnlyList<CellPosition> winningCells)
        {
            foreach (var cell in _cells)
            {
                cell.ToggleOverlayRT(true);
            }

            foreach (var position in winningCells)
            {
                var index = GetIndex(position.Row, position.Column);

                _cells[index].ToggleOverlayRT(false);
            }
        }

        private int GetIndex(int row, int column)
        {
            return row * BoardSize + column;
        }
    }
}