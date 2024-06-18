using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveParticle : MonoBehaviour
{
    [SerializeField] private GameObject flash;
    [SerializeField] private GameObject stoveFire;

    [SerializeField] private StoveCounter stoveCounter;

    private void Start() {
        stoveCounter.OnStovePaticleChanged += ShowParticles;
    }
    
    private void ShowParticles(object sender ,StoveCounter.OnStovePaticleChangedEvnetArgs e) {
        bool flag = e.state == StoveCounter.State.Fire || e.state == StoveCounter.State.Burned;
        stoveFire.SetActive(flag);
        flash.SetActive(flag);
    }

}
