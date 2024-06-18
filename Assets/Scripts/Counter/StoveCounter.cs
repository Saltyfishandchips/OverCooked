using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BasicCounter, IProgressBar
{

    [SerializeField] KitchenFiringObjectSO[] firingObjectSOArray;

    private Dictionary<KitchenObjectSO, KitchenObjectSO> firingObjectMap;

    private Dictionary<KitchenObjectSO, float> firingValueMap;
    
    private Dictionary<KitchenObjectSO, bool> BurnedObjectMap;

    private float firingProgressTime = 0f;

    //炉子的状态
    public enum State {
        Idle,
        Fire,
        Burned,
    }

    private State state;

    // 触发炉子的火花效果
    public event EventHandler<OnStovePaticleChangedEvnetArgs> OnStovePaticleChanged;

    public class OnStovePaticleChangedEvnetArgs: EventArgs {
        public State state;
    };

    public event EventHandler<IProgressBar.OnProgressChangedEventArgs> OnProgressChanged;

    private void Start() {
        state = State.Idle;
        firingObjectMap = new Dictionary<KitchenObjectSO, KitchenObjectSO>();
        firingValueMap = new Dictionary<KitchenObjectSO, float>();
        BurnedObjectMap = new Dictionary<KitchenObjectSO, bool>();
        foreach (KitchenFiringObjectSO kitchenFiringObjectSO in firingObjectSOArray) {
            firingObjectMap.Add(kitchenFiringObjectSO.inputObject, kitchenFiringObjectSO.outputObject);
            firingValueMap.Add(kitchenFiringObjectSO.inputObject, kitchenFiringObjectSO.firingTimemax);
            BurnedObjectMap.Add(kitchenFiringObjectSO.inputObject, kitchenFiringObjectSO.bBurned);
        }

    }

    private void Update() {
        
        firingProgressTime += Time.deltaTime;
        StoveStateChanged();
    }

    public override void Interact(Player player)
    {
        // 箱子上没有物品
        if (!HasKitchenObjectExisted()) {
            if (player.HasKitchenObjectExisted()) {
                // 玩家手中有物品
                KitchenObjectSO kitchenObjectSO = player.GetKitchenObject().GetKitchenObjectSO();
                if (firingObjectMap.ContainsKey(kitchenObjectSO)){
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    firingProgressTime = 0;
                    if (BurnedObjectMap.ContainsKey(kitchenObjectSO) && BurnedObjectMap[kitchenObjectSO]) {
                        state = State.Burned;
                    }
                    else {
                        state = State.Fire;
                    }   
                    

                    OnStovePaticleChanged?.Invoke(this, new OnStovePaticleChangedEvnetArgs{
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs{
                        progressNormalized = firingProgressTime
                    });
                }
            }
            else {
                // 玩家手中没有物品
            }
        }
        else {
            if (player.HasKitchenObjectExisted()) {
                // 玩家手中有物品，箱子有物品，两者互换
                KitchenObjectSO kitchenObjectSO = player.GetKitchenObject().GetKitchenObjectSO();

                if (player.GetKitchenObject() is PlateKitchenObject) {
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject.TryAddReceipt(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestorySelf();
                        ClearKitchenObject();
                        state = State.Idle;
                        firingProgressTime = 0;
                    }
                }
                else {
                    // 判断玩家手中的物品是否能到炉子中
                    if (firingObjectMap.ContainsKey(kitchenObjectSO)){
                        KitchenObject kitchenObject = player.GetKitchenObject();
                        GetKitchenObject().SetKitchenObjectParent(player);
                        kitchenObject.SetKitchenObjectParent(this);

                        if (BurnedObjectMap.ContainsKey(kitchenObjectSO) && BurnedObjectMap[kitchenObjectSO]) {
                            state = State.Burned;
                        }
                        else {
                            state = State.Fire;
                        }   
                        firingProgressTime = 0;
                    }
                }

            }
            else {
                // 玩家手中没有物品
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                firingProgressTime = 0;

            }
        }

        OnStovePaticleChanged?.Invoke(this, new OnStovePaticleChangedEvnetArgs{
            state = state
        });

        OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs{
            progressNormalized = firingProgressTime
        });
    }

    private void StoveStateChanged() {
        switch (state) {
            case State.Idle:
                break;
            case State.Fire:
                OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs{
                    progressNormalized = firingProgressTime / firingValueMap[GetKitchenObject().GetKitchenObjectSO()]
                });

                if (firingProgressTime >= firingValueMap[GetKitchenObject().GetKitchenObjectSO()]) {
                    KitchenObjectSO firedObjectSO = GetKitchenObject().GetKitchenObjectSO();
                    GetKitchenObject().DestorySelf();
                    KitchenObject.SpawnKitchenObject(firingObjectMap[firedObjectSO], this);
                    
                    state = State.Burned;
                    firingProgressTime = 0f;

                    OnStovePaticleChanged?.Invoke(this, new OnStovePaticleChangedEvnetArgs{
                        state = state
                    });
                }
                break;
            case State.Burned:
                OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs{
                    progressNormalized = firingProgressTime / firingValueMap[GetKitchenObject().GetKitchenObjectSO()]
                });

                if (firingProgressTime >= firingValueMap[GetKitchenObject().GetKitchenObjectSO()]) {
                    KitchenObjectSO burnedObjectSO = GetKitchenObject().GetKitchenObjectSO();

                    GetKitchenObject().DestorySelf();
                    KitchenObject.SpawnKitchenObject(firingObjectMap[burnedObjectSO], this);
                    state = State.Idle;
                    firingProgressTime = 0f;

                    OnStovePaticleChanged?.Invoke(this, new OnStovePaticleChangedEvnetArgs{
                        state = state
                    });
                }
                break;
        }

        
    }

    public override KitchenObject GetKitchenObject() {
        return kitchenObject;
    }
}
