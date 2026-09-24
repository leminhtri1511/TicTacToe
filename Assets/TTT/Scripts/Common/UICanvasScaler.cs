using UnityEngine;
using UnityEngine.UI;

namespace TTT.Scripts.Common
{
    [RequireComponent(typeof(RectTransform))]
    public class UICanvasScaler : MonoBehaviour
    {
        [SerializeField] private RectTransform _parentRectTransform;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private bool _canUpdate;

        private void Awake()
        {
            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }

            if (_parentRectTransform == null)
            {
                _parentRectTransform = _rectTransform.parent as RectTransform;
            }
        }

        private void Start()
        {
            CanvasScaler();
        }

        private void LateUpdate()
        {
            if (_canUpdate) CanvasScaler();
        }

        private float GetHeight(float scaler)
        {
            CanvasScaler canvasExtension = GlobalCanvas.Instance.CanvasScaler;
            float height = canvasExtension.referenceResolution.y * (2 - scaler) - Screen.safeArea.y;

            return height;
        }

        private void CanvasScaler()
        {
            CanvasScaler canvasExtension = GlobalCanvas.Instance.CanvasScaler;
            var referenceRatio = canvasExtension.referenceResolution.x / canvasExtension.referenceResolution.y;
            Vector2 currentCanvas = new Vector2(Screen.width, Screen.height);
            float currentRatio = currentCanvas.x / currentCanvas.y;
            float scaler = 1;

            if (currentRatio < referenceRatio)
            {
                scaler = currentRatio / referenceRatio;
                _rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                _rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                _rectTransform.sizeDelta = new Vector2(canvasExtension.referenceResolution.x, GetHeight(scaler));
            }
            else
            {
                ResetRectTransform();
            }

            _rectTransform.localScale = new Vector3(1 * scaler, 1 * scaler, 1);
        }

        private void ResetRectTransform()
        {
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.offsetMin = Vector2.zero;
            _rectTransform.offsetMax = Vector2.zero;
        }
    }
}