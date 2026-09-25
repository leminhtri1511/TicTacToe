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

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(RestartGame);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveAllListeners();
        }

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            _game = new TicTacToeGame();

            _boardViewHandle.InitializeBoard(OnCellClicked);

            StartGame();
        }

        private void StartGame()
        {
            _game.StartGame();
            ToggleRestartButton(false);
            _boardViewHandle.ResetBoard();

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

            ToggleRestartButton(true);

            if (_game.HasWinningCells)
            {
                _boardViewHandle.ShowWinningCells(_game.WinningCells);
            }
        }

        private void RestartGame()
        {
            StartGame();
        }

        private void ToggleRestartButton(bool isVisible)
        {
            _restartButton.gameObject.SetActive(isVisible);
        }
    }
}