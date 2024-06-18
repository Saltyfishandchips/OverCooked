using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenFiringObjectSO : ScriptableObject
{
    public KitchenObjectSO inputObject;
    public KitchenObjectSO outputObject;
    public float firingTimemax;
    public bool bBurned;
}
