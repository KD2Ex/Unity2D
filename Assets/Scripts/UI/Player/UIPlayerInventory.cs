using System;
using System.Collections.Generic;
using Interfaces;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

public class UIPlayerInventory : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private InputReader input;
    [SerializeField] private VisualTreeAsset cellElement;

    // ranged weapon
    // melee weapon
    // consumables

    
    private UIDocument document;
    private VisualElement inventoryTab;

    private bool isOpened;

    private const string enabledStyle = "inventory--enabled";
    private const string disabledStyle = "inventory--disabled";
    
    private void Awake()
    {
        document = GetComponent<UIDocument>();

        inventoryTab = document.rootVisualElement.Q("InventoryTab");

    }

    private void OnEnable()
    {
        input.TabEvent += Open;
    }

    private void OnDisable()
    {
        input.TabEvent -= Open;
    }

    private void Start()
    {
        inventoryTab.AddToClassList(enabledStyle);
        inventoryTab.AddToClassList(disabledStyle);
        //UpdateItems();
    }

    private void Open()
    {
        isOpened = !isOpened;
        
        if (isOpened)
        {
            UpdateItems();
            
            inventoryTab.RemoveFromClassList(disabledStyle);
            Time.timeScale = 0f;
            input.BlockInput();
        }
        else
        {
            inventoryTab.Clear();
            
            Time.timeScale = 1f;
            inventoryTab.AddToClassList(disabledStyle);
            input.UnblockInput();
        }
    }

    private void UpdateItems()
    {
        foreach (var item in inventory.Items)
        {
            VisualElement cellInstance = cellElement.Instantiate();
            var button = cellInstance.Q<Button>();
            
            if (button == null) continue;
            
            cellInstance.style.alignSelf = new StyleEnum<Align>(Align.FlexStart);
            button.style.backgroundImage = new StyleBackground(item.Icon);
            

            item.RegisterCallback(button);
            
            inventoryTab.Add(cellInstance);
        }
    }
    
}
