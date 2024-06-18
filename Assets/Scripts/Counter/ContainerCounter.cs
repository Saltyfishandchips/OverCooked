using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BasicCounter
{
    [SerializeField] KitchenObjectSO objectSO;

    public event EventHandler OnCounterOpenClose;

    public override void Interact(Player player) {
        if (player == null) {
            return;
        }

        if (!player.HasKitchenObjectExisted()) {
            KitchenObject.SpawnKitchenObject(objectSO, player);
            OnCounterOpenClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
