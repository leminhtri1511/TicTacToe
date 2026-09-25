using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TTT.Scripts.Data;
using UnityEngine;

namespace TTT.Scripts.Startup
{
    public class StartupBootstrapper : MonoBehaviour
    {
        [Header("Scenes")]
        [SerializeField] private string _mainSceneName = "MainScene";

        [Header("Services")]
        [SerializeField] private GameDataService _gameDataService;

        [Header("View")]
        [SerializeField] private LoadingView _loadingView;
        [SerializeField] private float _delayBeforeMainScene = 2f;

        private SceneLoader _sceneLoader;
        private CancellationToken _cancellationToken;

        private void Awake()
        {
            _sceneLoader = new SceneLoader();

            _cancellationToken = this.GetCancellationTokenOnDestroy();
        }

        private void Start()
        {
            StartupAsync(_cancellationToken).Forget();
        }

        private async UniTaskVoid StartupAsync(CancellationToken cancellationToken)
        {
            try
            {
                _loadingView.SetProgress(0f);

                await InitializeServicesAsync(cancellationToken);

                await LoadMainSceneAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("[Startup] Startup cancelled");
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);

                _loadingView.SetStatus("Startup failed");
            }
        }

        private async UniTask InitializeServicesAsync(CancellationToken cancellationToken)
        {
            _loadingView.SetStatus("Loading data...");

            await _gameDataService
                .InitializeAsync()
                .AttachExternalCancellation(cancellationToken);

            _loadingView.SetProgress(0.3f);
        }

        private async UniTask LoadMainSceneAsync(CancellationToken cancellationToken)
        {
            _loadingView.SetStatus("Loading game...");

            await _sceneLoader.LoadSceneAsync(
                _mainSceneName,
                progress =>
                {
                    float finalProgress = Mathf.Lerp(0.3f, 1f, progress);
                    _loadingView.SetProgress(finalProgress);
                },
                _delayBeforeMainScene,
                cancellationToken
            );
        }
    }
}