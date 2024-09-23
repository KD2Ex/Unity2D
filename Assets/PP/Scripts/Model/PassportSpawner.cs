using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassportSpawner : MonoBehaviour
{
    [SerializeField] private GameObject passport;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Transform parent;
    
    public void GetPassport()
    {
        Instantiate(passport, spawnPoint.transform.position, Quaternion.identity, parent);
    }
}
