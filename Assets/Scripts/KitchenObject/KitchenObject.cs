using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // 食材需要知道自己来源于哪个箱子
    private IKitchenObject kitchenObjectparent;

    public KitchenObjectSO GetkitchenObjectSO() {
        return kitchenObjectSO;
    }

    // 食材更换counter后父节点更换
    public void SetKitchenObjectParent(IKitchenObject kitchenObjectSecond) {
        if (kitchenObjectSecond == null) {
            return;
        }
        if (kitchenObjectparent != null && !kitchenObjectSecond.HasKitchenObjectExisted()) {
            kitchenObjectparent.ClearKitchenObject();
        }
        kitchenObjectparent = kitchenObjectSecond;
        kitchenObjectSecond.SetKitchenObject(this);

        transform.parent = kitchenObjectparent.GetKitchenObjectSpwanTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObject GetCounter() {
        return kitchenObjectparent;
    }

    public void DestorySelf() {
        kitchenObjectparent.ClearKitchenObject();
        kitchenObjectparent = null;
        Destroy(gameObject);
    }


    public static void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObject parent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(parent);
    }

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }
}
