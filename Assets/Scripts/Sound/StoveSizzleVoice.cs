using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSizzleVoice : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start() {
        stoveCounter.OnStovePaticleChanged += OnStovePaticleChangedEvent;
    }

    private void OnStovePaticleChangedEvent(object sender, StoveCounter.OnStovePaticleChangedEvnetArgs e) {
        if (e.state == StoveCounter.State.Fire || e.state == StoveCounter.State.Burned) {
            audioSource.Play();
        }
        else {
            audioSource.Pause();
        }
    }
}
