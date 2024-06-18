using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObject
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private EnhancedInput gameInput;
    [SerializeField] private Transform attachObject;

    public event EventHandler OnPickObject;

    private bool bIsWalking = false;
    private Vector3 lastInteractvec;
    // 玩家交互的箱子，用于高亮显示
    private BasicCounter counterSeleceted;
    
    // 调用有参数的Event
    public event EventHandler<OnSelectedCounterChangedArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedArgs: EventArgs {
        public BasicCounter counterSeleceted;
    }

    // 玩家手中是否有物品
    private KitchenObject kitchenObject;

    // 使用单例来统一管理箱子高亮显式，使用静态属性来当做单例
    public static Player Instance{ get; private set; }


    // Awake在脚本执行中比Start早，因此初始化部分放在Awake中，其他放在Start中，以免出现对象为空的情况
    private void Awake() {
        if (Instance != null) {
            throw new Exception("存在多个Player Instance!");
        }
        Instance = this;
    }

    private void Start() {
        //给Interact的Event注册subscribe函数
        gameInput.OnInteractEvent += InteractTrigger;
        gameInput.OnAlterInteractEvent += AlterInteractTrigger;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteract();
    }
    
    private void HandleMovement() {
        Vector2 inputVec = gameInput.GetInputVectorNormalized();
        Vector3 moveVec = new Vector3(inputVec.x, 0, inputVec.y);

        float playerHeight = 2f;
        float playerRaidus = 0.7f;
        float moveDistance = walkSpeed * Time.deltaTime;

        //胶囊体碰撞检测
        bool canMove = !QueryCapsuleCollision(playerHeight, playerRaidus, moveVec, moveDistance);


        if (!canMove) {
            //遇到障碍，将x向量与z向量分化，使其在遇到障碍后依旧可以往没有障碍的方向移动

            // X方向
            Vector3 moveVecX = new Vector3(moveVec.x, 0, 0).normalized; //这里需要归一化，否则会导致斜线方向移动的速度不一致
            canMove = moveVec.x != 0 && !QueryCapsuleCollision(playerHeight, playerRaidus, moveVecX, moveDistance);

            if (canMove) {
                //可以往X方向移动
                moveVec = moveVecX;
            }
            else {
                // Z方向
                Vector3 moveVecZ = new Vector3(0, 0, moveVec.z);
                canMove = moveVec.z != 0 && !QueryCapsuleCollision(playerHeight, playerRaidus, moveVecZ, moveDistance);

                if (canMove) {
                    moveVec = moveVecZ;
                }
                else {
                    // 不可以往任意方向移动
                }
            }
        }

        if (canMove) {
            transform.position += moveVec * moveDistance;
        }
       

        //旋转插值使用slerp，向量插值使用lerp
        transform.forward = Vector3.Slerp(transform.forward, moveVec, Time.deltaTime * rotationSpeed);

        bIsWalking = moveVec != Vector3.zero;        
    }

    private void InteractTrigger(object sender, EventArgs args) {
        if (!GameManager.Instance.IsGameInPlaying()) return;

        if (counterSeleceted != null) {
            counterSeleceted.Interact(this);
        }
    }

    private void AlterInteractTrigger(object sender, EventArgs args) {
        if (!GameManager.Instance.IsGameInPlaying()) return;

        if (counterSeleceted != null) {
            counterSeleceted.AlterInteract(this);
        }
    }

    private void HandleInteract() {
        Vector2 inputVec = gameInput.GetInputVectorNormalized();
        Vector3 moveVec = new Vector3(inputVec.x, 0, inputVec.y);
        
        if (moveVec != Vector3.zero) {
            lastInteractvec = moveVec;
        }

        float InteractDistance = 1f;
        if (Physics.Raycast(transform.position, lastInteractvec, out RaycastHit hitInfo, InteractDistance)) {
            if (hitInfo.transform.TryGetComponent(out BasicCounter counterInteract)) {
                SelectCounter(counterInteract);
            }
            else {
                //
                SelectCounter(null);
            }
        }
        else {
            SelectCounter(null);
        }
    }

    private void SelectCounter(BasicCounter counterInteract) {
        counterSeleceted = counterInteract;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedArgs{
            counterSeleceted = counterSeleceted
        });
    }

    public bool GetWalking() {
        return bIsWalking;
    }

    private bool QueryCapsuleCollision(float playerHeight, float playerRaidus, Vector3 moveVec, float moveDistance) {
        return Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRaidus, moveVec, moveDistance);
    }

    public Transform GetKitchenObjectSpwanTransform() {
        return attachObject;
    }

    public bool HasKitchenObjectExisted() {
        return kitchenObject != null;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject(){
        if (kitchenObject != null) {
            OnPickObject?.Invoke(this, EventArgs.Empty); 
        }
        return kitchenObject;
    }
    
}

