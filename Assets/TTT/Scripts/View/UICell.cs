using System;
using TMPro;
using TTT.Scripts.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace TTT.Scripts.View
{
    public class UICell : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;

        private int _row;
        private int _column;

        private Action<int, int> _onClicked;

        public void Initialize(int row, int column, Action<int, int> onClicked)
        {
            _row = row;
            _column = column;
            _onClicked = onClicked;

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnButtonClicked);

            SetState(CellState.Empty);
        }

        public void SetState(CellState state)
        {
            _text.text = state switch
            {
                CellState.Empty => "",
                CellState.X => "X",
                CellState.O => "O",
                _ => _text.text
            };

            _button.interactable = state == CellState.Empty;
        }

        public void SetInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }

        private void OnButtonClicked()
        {
            _onClicked?.Invoke(_row, _column);
        }
    }
}