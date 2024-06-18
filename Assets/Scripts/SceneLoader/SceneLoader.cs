using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        GameScene,
        LoadScene,
        MainMenu
    }

    private static Scene targetscene;

    public static void LoadScene(Scene sceneName) {
        targetscene = sceneName;
        SceneManager.LoadScene(Scene.LoadScene.ToString());
    }

    public static void LoadSceneCallBack() {
        SceneManager.LoadScene(targetscene.ToString());
    }    
}
