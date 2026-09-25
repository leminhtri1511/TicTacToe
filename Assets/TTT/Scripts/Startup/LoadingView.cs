using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TTT.Scripts.Startup
{
    public class LoadingView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private TextMeshProUGUI _statusText;
        // [SerializeField] private TextMeshProUGUI _progressText;

        [Header("Progress Config")]
        [SerializeField] private float _smoothTime = 0.2f;

        private float _targetProgress;
        private float _displayProgress;
        private float _velocity;

        private void Awake()
        {
            SetProgressImmediate(0f);
        }

        private void Update()
        {
            UpdateProgressVisual();
        }

        public void SetProgress(float progress)
        {
            _targetProgress = Mathf.Clamp01(progress);
        }

        public void SetProgressImmediate(float progress)
        {
            progress = Mathf.Clamp01(progress);

            _targetProgress = progress;
            _displayProgress = progress;
            _velocity = 0f;

            UpdateProgressUI();
        }

        public void SetStatus(string status)
        {
            if (_statusText == null) return;

            _statusText.text = status;
        }

        private void UpdateProgressVisual()
        {
            _displayProgress = Mathf.SmoothDamp(
                _displayProgress,
                _targetProgress,
                ref _velocity,
                _smoothTime,
                Mathf.Infinity,
                Time.unscaledDeltaTime
            );

            // Tránh SmoothDamp không bao giờ chạm chính xác target
            if (Mathf.Abs(_displayProgress - _targetProgress) < 0.001f)
            {
                _displayProgress = _targetProgress;
            }

            UpdateProgressUI();
        }

        private void UpdateProgressUI()
        {
            if (_progressSlider != null)
            {
                _progressSlider.value = _displayProgress;
            }

            // if (_progressText != null)
            // {
            //     int percentage = Mathf.RoundToInt(
            //         _displayProgress * 100f
            //     );
            //
            //     _progressText.text = $"{percentage}%";
            // }
        }
    }
}