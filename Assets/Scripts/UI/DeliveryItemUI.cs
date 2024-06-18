using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryItemUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform icon;
    
    private void Awake() {
        icon.gameObject.SetActive(false);
    }

    public void SetItemUI(ReceiptSO receiptSO) {
        recipeName.text = receiptSO.receiptName;

        foreach (Transform child in container) {
            if (child == icon)
                continue;
            Destroy(icon.gameObject);
        }

        List<KitchenObjectSO> receiptList = receiptSO.receiptList;
        foreach (KitchenObjectSO kitchenObjectSO in receiptList) {
            Transform iconTransform = Instantiate(icon, container);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.Icon;
            
        }
    }
}
