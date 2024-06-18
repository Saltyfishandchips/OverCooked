using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnhancedInput : MonoBehaviour
{
    public static EnhancedInput Instance;
    private GameInput gameInput;
    public event EventHandler OnInteractEvent;
    public event EventHandler OnAlterInteractEvent;
    public event EventHandler OnPause;

    public enum Binding {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        AlterInteract
    }

    private void Awake() {
        if (Instance != null) {
            throw new Exception ("EnhancedInput的Instance已存在!");
        }
        Instance = this;

        gameInput = new GameInput();
        gameInput.Enable();

        //交互键注册Event
        gameInput.Player.Interact.performed += InteractButtonPressed;
        gameInput.Player.AlterInteract.performed += AlterInteractButtonPressed;
        gameInput.Player.Pause.performed += PauseBttonPressed;
    }

    private void OnDestroy() {
        gameInput.Player.Interact.performed -= InteractButtonPressed;
        gameInput.Player.AlterInteract.performed -= AlterInteractButtonPressed;
        gameInput.Player.Pause.performed -= PauseBttonPressed;

        gameInput.Dispose();
    }

    public Vector2 GetInputVectorNormalized() {
        //使用增强输入
        Vector2 inputVec = gameInput.Player.Move.ReadValue<Vector2>();
        inputVec = inputVec.normalized;

        return  inputVec;
    }

    private void InteractButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext callback) {
        //pulisher的event广播给所有sub用户
        OnInteractEvent?.Invoke(this, EventArgs.Empty);
    }

    private void AlterInteractButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext callback) {
        //pulisher的event广播给所有sub用户
        OnAlterInteractEvent?.Invoke(this, EventArgs.Empty);
    }

    private void PauseBttonPressed(UnityEngine.InputSystem.InputAction.CallbackContext callback) {
        OnPause?.Invoke(this, EventArgs.Empty);
    }

    public string ShowBinding(Binding binding) {
        switch (binding) {
            case Binding.Move_Up:
                return gameInput.Player.Move.GetBindingDisplayString(1);
            case Binding.Move_Down:
                return gameInput.Player.Move.GetBindingDisplayString(2);
            case Binding.Move_Left:
                return gameInput.Player.Move.GetBindingDisplayString(3);
            case Binding.Move_Right:
                return gameInput.Player.Move.GetBindingDisplayString(4);
            case Binding.Interact:
                return gameInput.Player.Interact.GetBindingDisplayString(0);
            case Binding.AlterInteract:
                return gameInput.Player.AlterInteract.GetBindingDisplayString(0);
        }

        return null;
    }

    public void RebindKey(Binding binding, Action onActionRebound) {

        gameInput.Player.Disable();
        InputAction inputAction;
        int index;

        switch (binding) {
            default:
            case Binding.Move_Up:
                inputAction = gameInput.Player.Move;
                index = 1;
                break;
            case Binding.Move_Down:
                inputAction = gameInput.Player.Move;
                index = 2;
                break;
            case Binding.Move_Left:
                inputAction = gameInput.Player.Move;
                index = 3;
                break;
            case Binding.Move_Right:
                inputAction = gameInput.Player.Move;
                index = 4;
                break;
            case Binding.Interact:
                inputAction = gameInput.Player.Interact;
                index = 0;
                break;
            case Binding.AlterInteract:
                inputAction = gameInput.Player.AlterInteract;
                index = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(index).OnComplete(callback => {
            callback.Dispose();
            gameInput.Player.Enable();
            onActionRebound();
        }).Start();
    }
}
