using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] Transform IconTemplate;

    private void Awake() {
        IconTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        plateKitchenObject.OnPlateObjectVisualChanged += OnPlateObjectVisualChangedEvent;
    }

    private void OnPlateObjectVisualChangedEvent(object sender, PlateKitchenObject.OnPlateObjectVisualChangedEvnetArgs e) {
        UptdatePlateIcon();
    }

    private void UptdatePlateIcon() {
        
        foreach (Transform child in transform) {
            if (child == IconTemplate)
                continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetkitchenObjectSOList()) {
            Transform Icon = Instantiate(IconTemplate, transform);
            Icon.GetComponent<PlateIconSprite>().UpdateIcon(kitchenObjectSO);
            Icon.gameObject.SetActive(true);
        }
        
    }
}
