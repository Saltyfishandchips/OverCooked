using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BasicCounter, IProgressBar
{
    [SerializeField] KitchenCuttingObjectSO[] cuttingObjectSOArray;

    private Dictionary<KitchenObjectSO, KitchenObjectSO> cuttingObjectMap;

    private Dictionary<KitchenObjectSO, int> cuttingValueMap;
    int cuttingProgress;

    // 案板动画event
    public event EventHandler OnCuttingProgressAnimator;
    public event EventHandler<IProgressBar.OnProgressChangedEventArgs> OnProgressChanged;
    
    // 任意案板在切菜状态都会使用下面的Delegete
    public static event EventHandler OnAnyCutting;

    new public static void ResetStaticEvent() {
        OnAnyCutting = null;
    }


    private void Awake() {
        cuttingObjectMap = new Dictionary<KitchenObjectSO, KitchenObjectSO>();
        cuttingValueMap = new Dictionary<KitchenObjectSO, int>();
        foreach (KitchenCuttingObjectSO cuttingObjectSO in cuttingObjectSOArray) {
            cuttingObjectMap.Add(cuttingObjectSO.inputObject, cuttingObjectSO.outputObject);
            cuttingValueMap.Add(cuttingObjectSO.inputObject, cuttingObjectSO.cuttingProgressMax);
        }
    }

    public override void Interact(Player player) {
        // 箱子上没有物品
        if (!HasKitchenObjectExisted()) {
            if (player.HasKitchenObjectExisted()) {
                // 玩家手中有物品
                KitchenObjectSO kitchenObjectSO = player.GetKitchenObject().GetKitchenObjectSO();
                if (cuttingObjectMap.ContainsKey(kitchenObjectSO)){
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    // 触发进度条动画
                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs{
                        progressNormalized = (float)cuttingProgress / cuttingValueMap[kitchenObjectSO]
                    });

                }
            }
            else {
                // 玩家手中没有物品
            }
        }
        else {
            if (player.HasKitchenObjectExisted()) {
                // 玩家手中有物品，箱子有物品，两者互换
                KitchenObjectSO kitchenObjectSO = player.GetKitchenObject().GetKitchenObjectSO();
                if (cuttingObjectMap.ContainsKey(kitchenObjectSO)){
                    KitchenObject kitchenObject = player.GetKitchenObject();
                    GetKitchenObject().SetKitchenObjectParent(player);
                    kitchenObject.SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    // 触发进度条动画
                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs{
                        progressNormalized = (float)cuttingProgress / cuttingValueMap[kitchenObjectSO]
                    });
                }
            }
            else {
                // 玩家手中没有物品
                GetKitchenObject().SetKitchenObjectParent(player);
                cuttingProgress = 0;
                OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs{
                    progressNormalized = cuttingProgress
                });
            }
        }
    }

    public override void AlterInteract(Player player) {
        if (player == null) {
            return;
        }

        if (cuttingObjectSOArray == null) {
            throw new Exception("cuttingObject未选择!");
        }

        if (GetKitchenObject() != null) {
            KitchenObjectSO kitchenObjectSO = GetKitchenObject().GetKitchenObjectSO();
            // 只有可以切的食材才可以上案板
            if (cuttingObjectMap.ContainsKey(kitchenObjectSO)){
                cuttingProgress++;

                // 触发进度条动画
                OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs{
                    progressNormalized = (float)cuttingProgress / cuttingValueMap[kitchenObjectSO]
                });
                
                OnCuttingProgressAnimator?.Invoke(this, EventArgs.Empty);
                OnAnyCutting?.Invoke(this, EventArgs.Empty);
                
                // 菜墩切够数量
                if (cuttingProgress >= cuttingValueMap[kitchenObjectSO]) {
                    GetKitchenObject().DestorySelf();
                    KitchenObject.SpawnKitchenObject(cuttingObjectMap[kitchenObjectSO], this);
                }
                
            }
            
        }
        
    }

}
