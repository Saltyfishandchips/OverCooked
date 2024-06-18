using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicCounter : MonoBehaviour, IKitchenObject
{
    [SerializeField] private Transform counterTop;
    protected KitchenObject kitchenObject;

    public static event EventHandler OnDropObject;

    public static void ResetStaticEvent() {
        OnDropObject = null;
    }

    public virtual void Interact(Player player) {
        // Debug.LogError("BacicCounter的Interact被调用!");
    }

    public virtual void AlterInteract(Player player) {
        // Debug.LogError("BacicCounter的AlterInteract被调用!");
    }

    public Transform GetKitchenObjectSpwanTransform() {
        return counterTop;
    }

    public bool HasKitchenObjectExisted() {
        return kitchenObject != null;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public virtual KitchenObject GetKitchenObject(){
        if (kitchenObject != null) {
            OnDropObject?.Invoke(this, EventArgs.Empty);
        }
        return kitchenObject;
    }
}
