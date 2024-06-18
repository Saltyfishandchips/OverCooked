using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter :BasicCounter
{
    public override void Interact(Player player) {
        // 箱子上没有物品
        if (!HasKitchenObjectExisted()) {
            if (player.HasKitchenObjectExisted()) {
                // 玩家手中有物品
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else {
                // 玩家手中没有物品
            }
        }
        else {
            if (player.HasKitchenObjectExisted()) {
                // 玩家手中拿着盘子
                if (player.GetKitchenObject() is PlateKitchenObject) {
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject.TryAddReceipt(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestorySelf();
                        ClearKitchenObject();
                    }
                    
                }
                else {
                    // 桌子上是盘子,玩家手中是食材
                    if (GetKitchenObject() is PlateKitchenObject) {
                        PlateKitchenObject plateKitchenObject = GetKitchenObject() as PlateKitchenObject;
                        if (plateKitchenObject.TryAddReceipt(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestorySelf();
                            player.ClearKitchenObject();
                        }
                    }
                    else {
                        // 玩家手中有物品且不是盘子，箱子有物品，两者互换
                        KitchenObject kitchenObject = player.GetKitchenObject();
                        GetKitchenObject().SetKitchenObjectParent(player);
                        kitchenObject.SetKitchenObjectParent(this);
                    }
                    
                }

            }
            else {
                // 玩家手中没有物品
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


}
