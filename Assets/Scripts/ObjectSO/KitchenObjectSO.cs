using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    // 预制体
    public Transform prefab;
    // 2D图标
    public Sprite Icon;
    // 物品名称
    public string ObjectName;
}
