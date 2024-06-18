using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingTimerUI : MonoBehaviour
{
    [SerializeField] private Image playingTimer;

    private void Start() {
        GameManager.Instance.OnGametateChanged += OnGametateChangedEvent;
        gameObject.SetActive(false);
    }

    private void OnGametateChangedEvent(object sender, GameManager.OnCountDownTimerEventArgs e) {
        if (GameManager.Instance.IsGameInPlaying()) {
            Show();
            playingTimer.fillAmount = e.countDownTimer / GameManager.Instance.GetPlayingTimer();
        }
        else {
            Hide();
        }
    }
    
    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    } 
}
