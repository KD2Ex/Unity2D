using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;

public class GameController : MonoBehaviour
{
    public static bool connected;

    private IEnumerator CheckForControllers()
    {
        while (true) {

            var gamepad = Gamepad.current;
            var keyboard = Keyboard.current;

            if (gamepad is DualShockGamepad)
            {
                Debug.Log("dualshock");
            }
            if (gamepad is XInputController)
            {
                Debug.Log("xinput");
            }

            connected = gamepad is not null;
            yield return new WaitForSeconds(1f);
        }
    }
    
    private void Awake()
    {
        StartCoroutine(CheckForControllers());
    }
}
