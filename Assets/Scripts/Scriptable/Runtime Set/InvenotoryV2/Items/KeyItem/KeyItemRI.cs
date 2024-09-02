using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Key item", menuName = "SO/Items/KeyItems/Item")]
public class KeyItemRI : InventoryItemRI
{
    public KeyItemType type;
    public override void OnClick(ClickEvent e)
    {
        base.OnClick(e);
    }

    public override void Drop()
    {
        // can't drop it
    }
}