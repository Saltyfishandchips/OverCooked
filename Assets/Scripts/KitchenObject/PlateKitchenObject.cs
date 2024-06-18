using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    // 盘子中可以出现的食材
    [SerializeField] private List<KitchenObjectSO> vaildKitchenObjectList;

    // 盘子中现有的食材
    private List<KitchenObjectSO> kitchenObjectSOList;
    public event EventHandler<OnPlateObjectVisualChangedEvnetArgs> OnPlateObjectVisualChanged;
    public class OnPlateObjectVisualChangedEvnetArgs: EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddReceipt(KitchenObjectSO kitchenObjectSO) {
        // 盘子中食材不合法
        if (!vaildKitchenObjectList.Contains(kitchenObjectSO)) {
            return false;
        }

        // 食材重复放置到盘子中
        if (!kitchenObjectSOList.Contains(kitchenObjectSO)) {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnPlateObjectVisualChanged?.Invoke(this, new OnPlateObjectVisualChangedEvnetArgs{
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
        return false;
    }

    public List<KitchenObjectSO> GetkitchenObjectSOList() {
        return kitchenObjectSOList;
    }

}
