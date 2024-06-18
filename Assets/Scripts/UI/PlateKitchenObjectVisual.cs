using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObjectVisual : MonoBehaviour
{
    [Serializable]
    private struct VaildKitchenObject{
        public KitchenObjectSO kitchenObjectSO;
        public GameObject kitchenObject;
    }

    [SerializeField] private List<VaildKitchenObject> vaildKitchenObjectList;

    [SerializeField] private PlateKitchenObject plateKitchenObject;

    private void Start() {
        foreach (VaildKitchenObject vaildKitchenObject in vaildKitchenObjectList) {
            vaildKitchenObject.kitchenObject.SetActive(false);
        }
        plateKitchenObject.OnPlateObjectVisualChanged += OnPlateObjectVisualChangedEvent;
        
    }

    private void OnPlateObjectVisualChangedEvent(object sender, PlateKitchenObject.OnPlateObjectVisualChangedEvnetArgs e) {
        foreach (VaildKitchenObject vaildKitchenObject in vaildKitchenObjectList) {
            if (vaildKitchenObject.kitchenObjectSO == e.kitchenObjectSO) {
                vaildKitchenObject.kitchenObject.SetActive(true);
                break;
            }
        }
    }

    
}
