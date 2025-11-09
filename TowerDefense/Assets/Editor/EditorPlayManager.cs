#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

// 에디터 전용
[InitializeOnLoad]
public static partial class EditorPlayManager
{
    static EditorPlayManager()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        switch (state)
        {
            case PlayModeStateChange.ExitingEditMode:
                var allScenes = "";
                for (int i = 0; i < EditorSceneManager.sceneCount; i++)
                {
                    var scene = EditorSceneManager.GetSceneAt(i);
                    if (scene.isLoaded)
                        allScenes += scene.path + ";";
                }
                EditorPrefs.SetString("EditorPlayManager.OpenScenes", allScenes);

                // PlayMode 시작 시 AddressableScene으로 강제 지정
                var playScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/AddressableScene.unity");
                if (playScene != null)
                {
                    EditorSceneManager.playModeStartScene = playScene;
                }

                break;
            //case PlayModeStateChange.EnteredPlayMode:
            //    break;
        }
    }
}
#endif