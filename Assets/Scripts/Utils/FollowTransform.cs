using UnityEngine;

public class FollowTransform : MonoBehaviour
{

    [SerializeField] private Transform target;

    void LateUpdate()
    {
        var pos = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = pos;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
