using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "SO/Player")]
public class InputReader : ScriptableObject, PlayerInput.IMainActions, PlayerInput.IUIActions
{
    private PlayerInput _input;
    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction PrimaryEvent;
    public event UnityAction InteractEvent;
    public event UnityAction DashEvent;
    public event UnityAction<bool> AimEvent;
    public UnityAction TestEvent;
    public UnityAction TabEvent;
    
    public bool BlockMovementInput { get; set; }

    public void BlockInput()
    {
        _input.Main.Disable();
    }

    public void UnblockInput()
    {
        _input.Main.Enable();
    }

    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new PlayerInput();
            _input.Main.SetCallbacks(this);
            _input.UI.SetCallbacks(this);
        }
        
        _input.Enable();
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

}
