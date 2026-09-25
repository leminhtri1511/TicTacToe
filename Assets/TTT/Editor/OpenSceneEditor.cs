using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class OpenSceneEditor
{
    private static readonly string _scenePath = "Assets/Scenes/{0}.unity";

    [MenuItem("OpenScene/StartupScene", false, 0)]
    public static void StartupScene()
    {
        if (EditorApplication.isPlaying) return;
        if (!string.IsNullOrEmpty(SceneManager.GetActiveScene().name))
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            EditorSceneManager.OpenScene(string.Format(_scenePath, "StartupScene"), OpenSceneMode.Single);
    }

    [MenuItem("OpenScene/MainScene", false, 1)]
    public static void MainScene()
    {
        if (EditorApplication.isPlaying) return;
        if (!string.IsNullOrEmpty(SceneManager.GetActiveScene().name))
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            EditorSceneManager.OpenScene(string.Format(_scenePath, "MainScene"), OpenSceneMode.Single);
    }
}