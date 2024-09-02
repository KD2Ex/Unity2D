using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFadeController : MonoBehaviour
{
    [SerializeField] private Transform target;

    public void SetTransform()
    {
        transform.position = target.position;
    }
}
