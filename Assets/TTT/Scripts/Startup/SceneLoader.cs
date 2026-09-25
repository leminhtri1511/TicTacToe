using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace TTT.Scripts.Startup
{
    public class SceneLoader
    {
        public async UniTask LoadSceneAsync(string sceneName,
            Action<float> onProgress = null,
            float delayBeforeActivation = 0f,
            CancellationToken cancellationToken = default)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);

            if (operation == null)
            {
                throw new Exception($"Cannot load scene: {sceneName}");
            }

            operation.allowSceneActivation = false;

            while (operation.progress < 0.9f)
            {
                float progress = operation.progress / 0.9f;

                onProgress?.Invoke(progress);

                await UniTask.Yield();
            }

            onProgress?.Invoke(1f);

            if (delayBeforeActivation > 0f)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delayBeforeActivation), cancellationToken: cancellationToken);
            }

            operation.allowSceneActivation = true;

            await operation.ToUniTask(cancellationToken: cancellationToken);
        }
    }
}