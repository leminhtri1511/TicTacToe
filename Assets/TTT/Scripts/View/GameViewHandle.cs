using TMPro;
using TTT.Scripts.Logic;
using TTT.Scripts.Utils;
using UnityEngine;

namespace TTT.Scripts.View
{
    public class GameViewHandle : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI _statusText;

        private TicTacToeGame _game;

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
    }
}