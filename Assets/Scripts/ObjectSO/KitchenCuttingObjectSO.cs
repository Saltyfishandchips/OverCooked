using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenCuttingObjectSO : ScriptableObject
{
    public KitchenObjectSO inputObject;
    public KitchenObjectSO outputObject;
    public int cuttingProgressMax;

}
