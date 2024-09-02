using UnityEngine;

public class RuntimeItem : MonoBehaviour
{
    [SerializeField] private RuntimeSet<RuntimeItem> set;

    private void OnEnable()
    {
        set.Add(this);
    }

    private void OnDisable()
    {
        set.Remove(this);
    }

}
