using System;
using TMPro;
using TTT.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace TTT.Scripts.View
{
    public class UICell : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _overlayRectTransform;
        [SerializeField] private Image _outline;
        [SerializeField] private TextMeshProUGUI _text;

        [Header("Configs")]
        [SerializeField] private Color32 _xColor = new(255, 255, 255, a: 255);
        [SerializeField] private Color32 _oColor = new(255, 255, 255, a: 255);

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

            SetCellState(CellIdentity.Empty);
        }

        public void SetCellState(CellIdentity cellIdentity)
        {
            _outline.gameObject.SetActive(true);

            switch (cellIdentity)
            {
                case CellIdentity.X:
                    _text.text = "X";
                    SetColor(_xColor);
                    break;
                case CellIdentity.O:
                    _text.text = "O";
                    SetColor(_oColor);
                    break;
                case CellIdentity.Empty:
                    _text.text = "";
                    _outline.gameObject.SetActive(false);
                    ToggleOverlayRT(false);
                    break;
            }

            SetInteractable(cellIdentity == CellIdentity.Empty);
        }

        private void SetColor(Color32 color)
        {
            _outline.color = color;
            _text.color = color;
        }

        public void SetInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }

        private void OnButtonClicked()
        {
            _onClicked?.Invoke(_row, _column);
        }

        public void ToggleOverlayRT(bool isVisible)
        {
            _overlayRectTransform.gameObject.SetActive(isVisible);
        }
    }
}