
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu(fileName = "Ranged item", menuName = "SO/Items/Weapons/Ranged/Item")]
public class RangedRI : InventoryItemRI
{
    [SerializeField] private RangedWeaponData data;
    [SerializeField] private RangedInventory inventory;

    public RangedWeaponData Data => data;
    
    public override void OnClick(ClickEvent e)
    {
        base.OnClick(e);

        if (!inventory.items.Contains(this))
        {
            inventory.items.Add(this);
        }
        inventory.Equipped = this;
    }

    public override void Drop()
    {
        base.Drop();
    }
}