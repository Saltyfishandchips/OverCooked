using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BasicCounter
{
    public static event EventHandler OnTrashSound;

    new public static void ResetStaticEvent() {
        OnTrashSound = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObjectExisted()) {
            OnTrashSound?.Invoke(this, EventArgs.Empty);
            player.GetKitchenObject().DestorySelf();
        }
    }
}
