using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Consumable", menuName = "SO/Items/Consumable/Item")]
public class ConsumableRI : InventoryItemRI
{
    public ConsumableEffect Effect;
    public int MaxCharges;
    public bool RefillOnEnable;
    
    public int currentCharges;

    private void OnEnable()
    {
        if (RefillOnEnable) Refill();
    }

    public override void OnClick(ClickEvent e)
    {
        base.OnClick(e);

        if (currentCharges <= 0) return;

        --currentCharges;
        Effect.Execute();
    }

    public override void Drop()
    {
        base.Drop();
    }

    public void Refill()
    {
        currentCharges = MaxCharges;
    }
}