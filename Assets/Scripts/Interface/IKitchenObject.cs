using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObject
{
    public Transform GetKitchenObjectSpwanTransform();

    public bool HasKitchenObjectExisted();

    public void ClearKitchenObject();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();
}
