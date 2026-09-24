using UnityEngine;
using UnityEngine.UI;

namespace TTT.Scripts.Common
{
    public class GlobalCanvas : MonoBehaviour
    {
        public static GlobalCanvas Instance;
        [SerializeField] private Canvas _globalCanvas;
        [SerializeField] private CanvasScaler _canvasScaler;
        public Canvas Canvas => _globalCanvas;
        public CanvasScaler CanvasScaler => _canvasScaler;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            SetDefaultFPS();
        }

        private void SetDefaultFPS()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }
}