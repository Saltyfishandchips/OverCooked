using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public event EventHandler<OnCountDownTimerEventArgs> OnGametateChanged;
    public class OnCountDownTimerEventArgs: EventArgs {
        public float countDownTimer;
    }

    public event EventHandler OnGamePauseOn;
    public event EventHandler OnGamePauseOff;

    private enum State {
        WaitingToStart,
        CountDown,
        Playing,
        GameOver
    }

    private State gameState;

    [SerializeField] private float startTimer = 2f;
    [SerializeField] private float countDownTimer = 5f;
    [SerializeField] private float playingTimer = 100f;
    private float timer = 2f;

    private bool bPause = false;

    private void Awake() {
        if (Instance != null) {
            throw new Exception("GameManager的Instance已存在!");
        }
        Instance = this;
        gameState = State.WaitingToStart;
    }

    private void Start() {
        EnhancedInput.Instance.OnPause += OnPauseEvent;
    }

    private void Update() {
        GameStateChange();
    }

    private void GameStateChange() {
        switch (gameState) {
            case State.WaitingToStart:
                timer -= Time.deltaTime;
                if (timer <= 0) {
                    timer = countDownTimer;
                    gameState = State.CountDown;
                }
                break;
            case State.CountDown:
                timer -= Time.deltaTime;
                if (timer <= 0) {
                    timer = playingTimer;
                    gameState = State.Playing;
                }
                break;
            case State.Playing:
                timer -= Time.deltaTime;
                if (timer <= 0) {
                    timer = startTimer;
                    gameState = State.GameOver;
                }
                break;
            case State.GameOver:
                break;
        }

        OnGametateChanged?.Invoke(this, new OnCountDownTimerEventArgs{
            countDownTimer = timer
        });
    }

    public bool IsGameInPlaying() {
        return gameState == State.Playing;
    }

    public bool IsGameCountDown() {
        return gameState == State.CountDown;
    }

    public bool IsGameOver() {
        return gameState == State.GameOver;
    }

    public float GetPlayingTimer() {
        return playingTimer;
    }

    public void OnPauseEvent(object sender, EventArgs e) {
        GamePause();
    }

    public void GamePause() {
        bPause = !bPause;
        if (bPause) {
            Time.timeScale = 0f;
            OnGamePauseOn?.Invoke(this, EventArgs.Empty);
        }
        else {
            Time.timeScale = 1f;
            OnGamePauseOff?.Invoke(this, EventArgs.Empty);
        }
    }
}
