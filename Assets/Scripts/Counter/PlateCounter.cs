using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BasicCounter
{
    [SerializeField] private KitchenObjectSO plateObjectSO;

    // 当前盘子数量
    private int plateNum = 0;
    // 最大盘子数量
    private int plateNumMax = 5;
    // 生成盘子时间间隔
    private float spawnPlateTime = 4f;
    // 计时器
    private float time = 0;

    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemoved;

    private void Update() {
        time += Time.deltaTime;
        if (time >= spawnPlateTime) {
            if (plateNum < plateNumMax) {
                plateNum++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
                
            }
            time = 0f;
        }
    }

    public override void Interact(Player player)
    {
        if (!player.GetKitchenObject() && plateNum > 0) {
            KitchenObject.SpawnKitchenObject(plateObjectSO, player);
            plateNum--;
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}
