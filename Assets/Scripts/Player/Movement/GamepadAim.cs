using UnityEngine;

public class GamepadAim : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    [SerializeField] private float activateValue = .7f;
    
    private void OnEnable()
    {
        inputReader.LookEvent += Aim;
    }

    private void OnDisable()
    {
        inputReader.LookEvent -= Aim;
    }

    private void Aim(Vector2 direction)
    {
        if (!(Mathf.Abs(direction.x) > activateValue || Mathf.Abs(direction.y) > activateValue)) return;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.Euler(0f, 0f, angle + 90);
        transform.rotation = rotation;

    }
}
