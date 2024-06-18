using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    [SerializeField] private VoiceObjectSO voiceObjectSO;

    private readonly string EFFECT_VOLUM = "EffectVolum";

    public static VoiceManager Instance;

    private float volume  = 1f;

    private void Awake() {
        if (Instance != null) {
            throw new Exception("Voice Manager的Instance已存在!");
        }
        Instance = this;
    }

    private void Start() {
        ReceiptManager.Instance.OnDeliverySuccess += OnDeliverySuccessEvent;
        ReceiptManager.Instance.OnDeliveryFail += OnDeliveryFailEvent;
        CuttingCounter.OnAnyCutting += OnAnyCuttingEvent;
        TrashCounter.OnTrashSound += OnTrashSoundEvent;
        Player.Instance.OnPickObject += OnPickObjectEvent;
        BasicCounter.OnDropObject += OnDropObjectEvent;

        // 初始化保存的音量
        volume = PlayerPrefs.GetFloat(EFFECT_VOLUM, 1f);
    
    }

    private void OnDeliverySuccessEvent(object sender, EventArgs e) {
        SoundClipPlay(voiceObjectSO.delivberySuccess, DeliveryCounter.Instance.transform.localPosition);
    }

    private void OnDeliveryFailEvent(object sender, EventArgs e) {
        SoundClipPlay(voiceObjectSO.delivberyFail, DeliveryCounter.Instance.transform.localPosition);
    }

    private void OnAnyCuttingEvent(object sender, EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        SoundClipPlay(voiceObjectSO.chop, cuttingCounter.transform.localPosition);
    }

    private void OnTrashSoundEvent(object sender, EventArgs e) {
        TrashCounter trashCounter = sender as TrashCounter;
        SoundClipPlay(voiceObjectSO.trash, trashCounter.transform.localPosition);
    }

    private void OnPickObjectEvent(object sender, EventArgs e) {
        SoundClipPlay(voiceObjectSO.objectPickup, Player.Instance.transform.localPosition);
    }

    private void OnDropObjectEvent(object sender, EventArgs e) {
        BasicCounter basicCounter = sender as BasicCounter;
        SoundClipPlay(voiceObjectSO.objectPickup, basicCounter.transform.localPosition);
    }

    public void SoundClipPlay(AudioClip[] audioClipArray, Vector3 position) {
        AudioSource.PlayClipAtPoint(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void FootStepSoundPlay(Vector3 position) {
        AudioSource.PlayClipAtPoint(voiceObjectSO.footStep[UnityEngine.Random.Range(0, voiceObjectSO.footStep.Length)], position, volume);
    }
    
    public void SetVolume(float vol) {
        volume = vol;
        PlayerPrefs.SetFloat(EFFECT_VOLUM, vol);
    }
}
