using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSprite : MonoBehaviour
{
    [SerializeField] Image icon;

    public void UpdateIcon(KitchenObjectSO kitchenObjectSO) {
        icon.sprite = kitchenObjectSO.Icon;
    }
}
