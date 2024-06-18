using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ReceiptListSO : ScriptableObject
{
    [SerializeField] public List<ReceiptSO> receiptSOList;
}
