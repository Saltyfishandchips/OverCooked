using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button quitButton;

    private void Awake() {
        BasicCounter.ResetStaticEvent();
        CuttingCounter.ResetStaticEvent();
        TrashCounter.ResetStaticEvent();

        playButton.onClick.AddListener(() => {
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }
}
