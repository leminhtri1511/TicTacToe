using TTT.Scripts.Data;
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
        [SerializeField] private Button _clearGameDataButton;

        private TicTacToeGame _game;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(RestartGame);
            _clearGameDataButton.onClick.AddListener(ClearGameData);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveAllListeners();
            _clearGameDataButton.onClick.RemoveAllListeners();
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

            SaveMatchResult();

            _boardViewHandle.SetBoardInteractable(false);

            ToggleRestartButton(true);

            if (_game.HasWinningCells)
            {
                _boardViewHandle.ShowWinningCells(_game.WinningCells);
            }

            _gameViewHandle.SetGameData();
        }

        private void SaveMatchResult()
        {
            var data = GameDataService.Instance.Data;

            switch (_game.GameState)
            {
                case GameState.XWin:
                    data.XWinCount++;
                    break;

                case GameState.OWin:
                    data.OWinCount++;
                    break;

                case GameState.Draw:
                    data.DrawCount++;
                    break;

                default:
                    return;
            }

            GameDataService.Instance.Save();
        }

        private void RestartGame()
        {
            StartGame();
        }

        private void ClearGameData()
        {
            GameDataService.Instance.ResetData();
            _gameViewHandle.SetGameData();
        }

        private void ToggleRestartButton(bool isVisible)
        {
            _restartButton.gameObject.SetActive(isVisible);
        }
    }
}