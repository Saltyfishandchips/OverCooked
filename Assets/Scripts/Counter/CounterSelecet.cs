using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CounterSelecet : MonoBehaviour
{
    [SerializeField] BasicCounter counterInteract;
    [SerializeField] GameObject[] counterSelectedEffectArray;
    private void Start() {
        Player.Instance.OnSelectedCounterChanged += SelectedCounterChanged;
    }

    private void SelectedCounterChanged(object sender, Player.OnSelectedCounterChangedArgs args) {
        foreach (GameObject counterSelectedEffect in counterSelectedEffectArray) {
            counterSelectedEffect.SetActive(counterInteract == args.counterSeleceted);
        }
        
    }
}
