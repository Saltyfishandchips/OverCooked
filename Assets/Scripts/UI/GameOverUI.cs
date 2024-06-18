using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI menuNumText;

    private void Start() {
        GameManager.Instance.OnGametateChanged += OnGameOverStateEvent;
        gameObject.SetActive(false);
    }

    private void OnGameOverStateEvent(object sender, GameManager.OnCountDownTimerEventArgs e) {
        if (GameManager.Instance.IsGameOver()) {
            gameObject.SetActive(true);
            menuNumText.text = ReceiptManager.Instance.GetRecipeNum().ToString();
        }
        
    }
}
