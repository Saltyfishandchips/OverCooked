using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button volumSettingButton;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.GamePause();
        });

        mainMenuButton.onClick.AddListener(() => {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenu);
        });
        volumSettingButton.onClick.AddListener(() => {
            VolumSettingUI.Instance.Show();
        });
    }

    private void Start() {
        GameManager.Instance.OnGamePauseOn += OnGamePauseOnEvent;
        GameManager.Instance.OnGamePauseOff += OnGamePauseOffEvent;
        Hide();
    }

    private void OnGamePauseOnEvent(object sender, EventArgs e) {
        Show();
    }

    private void OnGamePauseOffEvent(object sender, EventArgs e) {
        Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
} 
