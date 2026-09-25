using TTT.Scripts.Logic;
using TTT.Scripts.Utils;
using TTT.Scripts.View;
using UnityEngine;
using UnityEngine.UI;

namespace TTT.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("Controllers")]
        [SerializeField] private BoardViewHandle _boardViewHandle;
        [SerializeField] private GameViewHandle _gameViewHandle;

        [Header("UI")]
        [SerializeField] private Button _restartButton;

        private TicTacToeGame _game;

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            _game = new TicTacToeGame();

            _boardViewHandle.Initialize(OnCellClicked);

            _restartButton.onClick.RemoveAllListeners();
            _restartButton.onClick.AddListener(RestartGame);


            StartGame();
        }

        private void StartGame()
        {
            _restartButton.gameObject.SetActive(false);
            _game.StartGame();

            _boardViewHandle.ResetBoard();
            _boardViewHandle.SetBoardInteractable(true);

            UpdateGameStatus();
        }

        private void OnCellClicked(int row, int column)
        {
            var moveSuccess = _game.MakeMove(row, column);

            if (!moveSuccess) return;

            UpdateCell(row, column);
            UpdateGameStatus();
        }

        private void UpdateCell(int row, int column)
        {
            var state = _game.GetCell(row, column);

            _boardViewHandle.UpdateCell(row, column, state);
        }

        private void UpdateGameStatus()
        {
            _gameViewHandle.UpdateGameView(_game);

            GameFinishedChecking();
        }

        private void GameFinishedChecking()
        {
            if (_game.GameState == GameState.Playing) return;

            _boardViewHandle.SetBoardInteractable(false);

            _restartButton.gameObject.SetActive(true);

            if (_game.HasWinningCells)
            {
                _boardViewHandle.ShowWinningCells(_game.WinningCells);
            }
        }

        private void RestartGame()
        {
            StartGame();
        }
    }
}