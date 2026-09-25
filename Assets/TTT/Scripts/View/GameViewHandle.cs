using TMPro;
using TTT.Scripts.Data;
using TTT.Scripts.Logic;
using TTT.Scripts.Utils;
using UnityEngine;

namespace TTT.Scripts.View
{
    public class GameViewHandle : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private TextMeshProUGUI _xWinText;
        [SerializeField] private TextMeshProUGUI _oWinText;
        [SerializeField] private TextMeshProUGUI _drawText;

        private TicTacToeGame _game;

        private void Start()
        {
            SetGameData();
        }

        public void UpdateGameView(TicTacToeGame game)
        {
            _game = game;

            switch (game.GameState)
            {
                case GameState.Playing:
                    UpdateStatusText();
                    break;
                case GameState.XWin:
                    ShowWinner("X");
                    SetGameData();
                    break;
                case GameState.OWin:
                    ShowWinner("O");
                    SetGameData();
                    break;
                case GameState.Draw:
                    ShowDraw();
                    SetGameData();
                    break;
            }
        }

        private void UpdateStatusText()
        {
            _statusText.text = $"{_game.CurrentPlayer} Turn";
        }

        private void ShowWinner(string player)
        {
            _statusText.text = $"{player} Win!";
        }

        private void ShowDraw()
        {
            _statusText.text = "Draw!";
        }

        public void SetGameData()
        {
            var gameData = GameDataService.Instance.Data;

            _xWinText.text = gameData.XWinCount.ToString();
            _oWinText.text = gameData.OWinCount.ToString();
            _drawText.text = gameData.DrawCount.ToString();
        }
    }
}