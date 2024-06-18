using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepVoice : MonoBehaviour
{
    [SerializeField] Player player;
    float footStepTime = 0;
    float footStepTimeMax = .1f;

    private void Update() {
        footStepTime += Time.deltaTime;
        if (footStepTime >= footStepTimeMax) {
            footStepTime = 0f;

            if (player.GetWalking()) {
                VoiceManager.Instance.FootStepSoundPlay(player.transform.localPosition);
            }
        }

    }
}
