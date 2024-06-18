using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 只负责盘子Counter的视觉效果
public class PlateCounterVisual : MonoBehaviour
{
    // 预制体只有视觉效果，没有KitchenObjectSO等功能
    [SerializeField] private GameObject platePrefab;
    [SerializeField] private Transform plateSpawnPoint;
    [SerializeField] private PlateCounter plateCounter;
    private List<GameObject> plateObjectArray;

    private void Start() {
        plateObjectArray = new List<GameObject>();
        plateCounter.OnPlateSpawn += OnPlateSpawnEvent;
        plateCounter.OnPlateRemoved += OnPlateRemovedEvent;
    }

    private void OnPlateSpawnEvent(object sender, EventArgs e) {
        Transform plateTransform = Instantiate(platePrefab.transform, plateSpawnPoint);
        float plateOffset = .1f;
        plateTransform.localPosition = new Vector3(0, plateOffset * plateObjectArray.Count, 0);
        plateObjectArray.Add(plateTransform.gameObject);
    }

    private void OnPlateRemovedEvent(object sender, EventArgs e) {
        GameObject plateGameobject = plateObjectArray[plateObjectArray.Count - 1];
        plateObjectArray.Remove(plateGameobject);
        Destroy(plateGameobject);
    }


}
