using Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class RangedWeaponInventoryItem : WeaponInventoryItem
{
    [SerializeField] private RangedWeaponData stats;
    //[SerializeField] private RangedInventory inventory;
    public RangedWeaponData Weapon => stats;
    
    private IVisitor visitor;
    
    public override void RegisterCallback(VisualElement button)
    {
        button.RegisterCallback<ClickEvent>(Equip);
    }

    private void Equip(ClickEvent e)
    {
        visitor.Visit(this);
    }

    public override void Accept(IVisitor visitor)
    {
        this.visitor = visitor;
        if (AutoEquip) visitor.Visit(this);
    }
}