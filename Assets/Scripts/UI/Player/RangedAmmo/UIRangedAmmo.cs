using System;
using TMPro;
using UnityEngine;

public class UIRangedAmmo : MonoBehaviour
{
    [SerializeField] private RangedInventory inventory;
    [SerializeField] private TextMeshProUGUI textMesh;

    private void Awake()
    {
    }

    private void Start()
    {
        Debug.Log(textMesh.text);
        Debug.Log(inventory.Equipped.Data.CurrentAmmo.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = inventory.Equipped.Data.CurrentAmmo.ToString();
    }
}
