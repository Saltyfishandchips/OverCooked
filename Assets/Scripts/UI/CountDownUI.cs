using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private void Awake() {
        countDownText.gameObject.SetActive(false);
    }

    private void Start() {
        GameManager.Instance.OnGametateChanged += OnCountDownTimerEvent;
    }

    private void OnCountDownTimerEvent(object sender, GameManager.OnCountDownTimerEventArgs e) {

        if (GameManager.Instance.IsGameCountDown()) {
            countDownText.gameObject.SetActive(true);
            countDownText.text = Math.Ceiling(e.countDownTimer).ToString();
        }
        else {
            countDownText.gameObject.SetActive(false);
        }
        
    }

}
