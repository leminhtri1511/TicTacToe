using System;
using TTT.Scripts.Logic;
using TTT.Scripts.Manager;
using UnityEngine;

namespace TTT.Scripts.View
{
    public class TicTacToeBoardView : MonoBehaviour
    {
        private const int BoardSize = 3;

        [SerializeField]
        private UICell[] _cells;

        [Header("Winning Line")]
        [SerializeField]
        private RectTransform _winningLine;

        public void Initialize(Action<int, int> onCellClicked)
        {
            if (_cells.Length != BoardSize * BoardSize)
            {
                Debug.LogError(
                    $"TicTacToe Board requires {BoardSize * BoardSize} cells."
                );

                return;
            }

            for (int row = 0; row < BoardSize; row++)
            {
                for (int column = 0; column < BoardSize; column++)
                {
                    int index = GetIndex(row, column);

                    _cells[index].Initialize(
                        row,
                        column,
                        onCellClicked
                    );
                }
            }

            HideWinningLine();
        }

        public void UpdateCell(int row,
            int column,
            CellState state)
        {
            int index = GetIndex(row, column);

            _cells[index].SetState(state);
        }

        public void ResetBoard()
        {
            foreach (UICell cell in _cells)
            {
                cell.SetState(CellState.Empty);
            }

            HideWinningLine();
        }

        public void SetBoardInteractable(bool interactable)
        {
            foreach (UICell cell in _cells)
            {
                cell.SetInteractable(interactable);
            }
        }

        public void ShowWinningLine(WinningLine winningLine)
        {
            int startIndex =
                GetIndex(
                    winningLine.StartRow,
                    winningLine.StartColumn
                );

            int endIndex =
                GetIndex(
                    winningLine.EndRow,
                    winningLine.EndColumn
                );

            var startCell =
                _cells[startIndex].GetComponent<RectTransform>();

            var endCell =
                _cells[endIndex].GetComponent<RectTransform>();

            var startWorldPosition =
                startCell.TransformPoint(startCell.rect.center);

            var endWorldPosition =
                endCell.TransformPoint(endCell.rect.center);

            var boardRect =
                transform as RectTransform;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(boardRect,
                RectTransformUtility.WorldToScreenPoint(null, startWorldPosition),
                null,
                out var startLocalPosition
            );

            RectTransformUtility.ScreenPointToLocalPointInRectangle(boardRect,
                RectTransformUtility.WorldToScreenPoint(null, endWorldPosition),
                null,
                out var endLocalPosition
            );

            var direction =
                endLocalPosition - startLocalPosition;

            var distance =
                direction.magnitude;

            var center =
                (startLocalPosition + endLocalPosition) / 2f;

            var angle =
                Mathf.Atan2(
                    direction.y,
                    direction.x
                ) * Mathf.Rad2Deg;

            _winningLine.gameObject.SetActive(true);

            _winningLine.anchoredPosition =
                center;

            _winningLine.sizeDelta =
                new Vector2(
                    distance,
                    _winningLine.sizeDelta.y
                );

            _winningLine.localRotation =
                Quaternion.Euler(
                    0f,
                    0f,
                    angle
                );
        }

        public void HideWinningLine()
        {
            _winningLine.gameObject.SetActive(false);
        }

        private int GetIndex(int row, int column)
        {
            return row * BoardSize + column;
        }
    }
}