using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    [SerializeField] private GameObject _crosshair;

    private void Start()
    {
        _crosshair.SetActive(false);
    }

    public void OnAim(bool aiming)
    {
        _crosshair.SetActive(aiming);
    }
}
