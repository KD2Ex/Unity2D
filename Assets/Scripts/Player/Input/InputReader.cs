using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "SO/Player")]
public class InputReader : ScriptableObject, PlayerInput.IMainActions, PlayerInput.IUIActions, PlayerInput.IPauseMenuActions
{
    private PlayerInput _input;
    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction PrimaryEvent;
    public event UnityAction InteractEvent;
    public event UnityAction DashEvent;
    public event UnityAction<bool> AimEvent;
    public UnityAction EscEvent;
    public UnityAction TestEvent;
    public UnityAction TabEvent;
    public UnityAction<Vector2> LookEvent;
    public UnityAction ShootEvent;
    
    public bool BlockMovementInput { get; set; }

    public void BlockMainInput()
    {
        _input.Main.Disable();
    }

    public void UnblockMainInput()
    {
        _input.Main.Enable();
    }

    public void BlockAllGameplayInput()
    {
        _input.Main.Disable();
        _input.UI.Disable();
    }
    
    public void UnblockAllGameplayInput()
    {
        _input.Main.Enable();
        _input.UI.Enable();
    }


    private void OnEnable()
    {
        var inputBinding = new InputBinding("");
        //_input.Main.Primary.actionMap.AddBinding(new InputBinding());

        
        if (_input == null)
        {
            _input = new PlayerInput();
            _input.Main.SetCallbacks(this);
            _input.UI.SetCallbacks(this);
            _input.PauseMenu.SetCallbacks(this);
        }
        
        _input.Enable();
        
        foreach (var item in _input.Main.Primary.bindings)
        {
            Debug.Log(item.path);
        }
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public void OnTest(InputAction.CallbackContext context)
    {
        if (TestEvent != null && context.started)
        {
            TestEvent.Invoke();
        }
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (TabEvent != null && context.started)
        {
            TabEvent.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (BlockMovementInput) return;
        
        var value = context.ReadValue<Vector2>();
        MoveEvent?.Invoke(value);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        LookEvent?.Invoke(value);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (ShootEvent != null && context.started)
        {
            ShootEvent.Invoke();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (DashEvent != null && context.started)
        {
            DashEvent.Invoke();
        }
    }

    public void OnPrimary(InputAction.CallbackContext context)
    {
        if (PrimaryEvent != null && context.started)
        {
            PrimaryEvent.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (InteractEvent != null && context.started)
        {
            InteractEvent.Invoke();
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        bool result = false;
        if (AimEvent == null) return;

        if (context.started)
        {
            AimEvent.Invoke(true);
        }

        if (context.canceled)
        {
            AimEvent.Invoke(false);
        }
    }
    
    public void OnPause(InputAction.CallbackContext context)
    {
        if (EscEvent != null && context.started)
        {
            EscEvent.Invoke();
        }
    }

}
