using System;
using System.Collections.Generic;
using UnityEngine;


public class ReceiptManager : MonoBehaviour
{
    [SerializeField] private ReceiptListSO receiptListSO;

    public static ReceiptManager Instance {get; private set;}

    private List<ReceiptSO> receiptSOList;

    // 最大菜单数
    [SerializeField] private int receiptNumMax = 5;

    // 菜单间隔时间
    private float receiptTimeMax = 4f;
    private float  receiptTime;

    // 通知UI事件
    public event EventHandler OnMenuAdd;
    public event EventHandler OnMenuComplete;

    // 通知音效事件
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFail;

    int reciptCompleteNum = 0;

    private void Awake() {
        if (Instance != null) {
            throw new Exception("存在多个ReceiptManger Instance!");
        }
        Instance = this;
        receiptSOList = new List<ReceiptSO>();
    }

    private void Update() {
        receiptTime += Time.deltaTime;
        if (receiptTime >= receiptTimeMax) {
            if (receiptSOList.Count < receiptNumMax) {
                ReceiptSO receipt = receiptListSO.receiptSOList[UnityEngine.Random.Range(0,receiptListSO.receiptSOList.Count)];
                receiptSOList.Add(receipt);

                OnMenuAdd?.Invoke(this, EventArgs.Empty);
            }
            receiptTime = 0f;
        }

    }

    public void CheckReceipt(PlateKitchenObject plateKitchenObject) {
        // 盘子的KitchenObjectSO
        List<KitchenObjectSO> plateKitchenObjectSOList = plateKitchenObject.GetkitchenObjectSOList();


        for (int i = 0; i < receiptSOList.Count; ++i) {
            bool bSameReceipt = true;
            if (plateKitchenObjectSOList.Count == receiptSOList[i].receiptList.Count) {
                foreach (KitchenObjectSO receiptKitchenObjectSO in receiptSOList[i].receiptList) {
                    bool bSameObjectSO = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObjectSOList) {
                        if (plateKitchenObjectSO == receiptKitchenObjectSO) {
                            // 有相同的食材SO
                            bSameObjectSO = true;
                            break;
                        }
                    }

                    // 配方不一致
                    if (!bSameObjectSO) {
                        bSameReceipt = false;
                    }
                }

                if (bSameReceipt) {
                    reciptCompleteNum++;

                    receiptSOList.Remove(receiptSOList[i]);
                    
                    OnMenuComplete?.Invoke(this, EventArgs.Empty);
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
            
        }
    
        OnDeliveryFail?.Invoke(this, EventArgs.Empty);
    }
    
    public List<ReceiptSO> GetreceiptSOList() {
        return receiptSOList;
    }

    public int GetRecipeNum() {
        return reciptCompleteNum;
    }

}
