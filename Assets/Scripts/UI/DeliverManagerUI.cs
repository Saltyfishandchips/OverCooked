using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliverManagerUI : MonoBehaviour
{
    [SerializeField] private Transform recipeTemplate;
    [SerializeField] private Transform container;
    
    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        ReceiptManager.Instance.OnMenuAdd += OnMenuAddEvent;
        ReceiptManager.Instance.OnMenuComplete += OnMenuCompleteEvent;
        
        UpdateVisual();
    }

    private void OnMenuAddEvent(object sender, EventArgs e) {
        UpdateVisual();
    }

    private void OnMenuCompleteEvent(object sender, EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in container) {
            if (child == recipeTemplate) {
                continue;
            }
            Destroy(child.gameObject);
        }
        
        List<ReceiptSO> receiptSOList = ReceiptManager.Instance.GetreceiptSOList();
        foreach (ReceiptSO receiptSO in receiptSOList) {
            Transform item = Instantiate(recipeTemplate, container);
            item.gameObject.SetActive(true);
            item.GetComponent<DeliveryItemUI>().SetItemUI(receiptSO);
        }

    }

}
