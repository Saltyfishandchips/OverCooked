using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BasicCounter
{
    // 一局只有一个传输口
    public static DeliveryCounter Instance;

    private void Awake() {
        if (Instance != null) {
            throw new Exception("DeliveryCounter的Instance已存在!");
        }
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.GetKitchenObject() is PlateKitchenObject) {
            PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
            ReceiptManager.Instance.CheckReceipt(plateKitchenObject);
            player.GetKitchenObject().DestorySelf();
            player.ClearKitchenObject();
        }
    }
}
