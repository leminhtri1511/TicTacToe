using TMPro;
using TTT.Scripts.Logic;
using TTT.Scripts.View;
using UnityEngine;
using UnityEngine.UI;

namespace TTT.Scripts.Manager
{
    public enum CellState
    {
        Empty,
        X,
        O
    }

    public enum GameState
    {
        Playing,
        XWin,
        OWin,
        Draw
    }

    public class GameManager : MonoBehaviour
    {
        [Header("Board")]
        [SerializeField] private TicTacToeBoardView _boardView;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private Button _restartButton;

        private TicTacToeGame _game;

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            _game = new TicTacToeGame();

            _boardView.Initialize(OnCellClicked);

            _restartButton.onClick.RemoveAllListeners();
            _restartButton.onClick.AddListener(RestartGame);

            StartGame();
        }

        private void StartGame()
        {
            _game.StartGame();

            _boardView.ResetBoard();
            _boardView.SetBoardInteractable(true);

            UpdateStatusText();
        }

        private void OnCellClicked(int row, int column)
        {
            bool moveSuccess =
                _game.MakeMove(row, column);

            if (!moveSuccess) return;

            UpdateCell(row, column);

            UpdateGameView();
        }

        private void UpdateCell(int row, int column)
        {
            CellState state =
                _game.GetCell(row, column);

            _boardView.UpdateCell(
                row,
                column,
                state
            );
        }

        private void UpdateGameView()
        {
            switch (_game.State)
            {
                case GameState.Playing:
                    UpdateStatusText();
                    break;

                case GameState.XWin:
                    ShowWinner("X");
                    break;

                case GameState.OWin:
                    ShowWinner("O");
                    break;

                case GameState.Draw:
                    ShowDraw();
                    break;
            }
        }

        private void UpdateStatusText()
        {
            _statusText.text =
                $"Turn: {_game.CurrentPlayer}";
        }

        private void ShowWinner(string player)
        {
            _statusText.text =
                $"{player} Win!";

            _boardView.SetBoardInteractable(false);
            _boardView.ShowWinningLine(_game.WinningLine);
        }

        private void ShowDraw()
        {
            _statusText.text = "Draw!";

            _boardView.SetBoardInteractable(false);
        }

        private void RestartGame()
        {
            StartGame();
        }
    }
}