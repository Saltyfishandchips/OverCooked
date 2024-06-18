using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ReceiptSO : ScriptableObject
{
    [SerializeField] public List<KitchenObjectSO> receiptList;
    public string receiptName;
}
