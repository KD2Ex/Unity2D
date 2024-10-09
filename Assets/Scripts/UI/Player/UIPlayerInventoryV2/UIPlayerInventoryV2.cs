using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIPlayerInventoryV2 : MonoBehaviour
{
    [SerializeField] private PlayerInventoryRS inventory;
    [SerializeField] private VisualTreeAsset cell;
    [SerializeField] private InputReader input;
    
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

    
    private void Open()
    {
        isOpened = !isOpened;
        
        if (isOpened)
        {
            UpdateItems();
            
            inventoryTab.RemoveFromClassList(disabledStyle);
            Time.timeScale = 0f;
            input.BlockMainInput();
        }
        else
        {
            inventoryTab.Clear();
            
            Time.timeScale = 1f;
            inventoryTab.AddToClassList(disabledStyle);
            input.UnblockMainInput();
        }
    }
    private void Start()
    {
        inventoryTab.AddToClassList(enabledStyle);
        inventoryTab.AddToClassList(disabledStyle);
        //UpdateItems();
    }

    
    private void UpdateItems()
    {
        foreach (var item in inventory.Items)
        {
            VisualElement instance = cell.Instantiate();
            
            var button = instance.Q<Button>();
            
            button.style.backgroundImage = new StyleBackground(item.Icon);
            button.RegisterCallback<ClickEvent>(item.OnClick);

            if (item.Stackable)
            {
                // show amount
            }
            
            inventoryTab.Add(instance);
        }
    }
}