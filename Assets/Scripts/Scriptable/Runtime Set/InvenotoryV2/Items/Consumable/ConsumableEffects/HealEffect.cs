
using UnityEngine;

[CreateAssetMenu(fileName = "Heal effect", menuName = "SO/Items/Consumable/Effect/Heal")]
public class HealEffect : ConsumableEffect
{
    public float amount;
    public FloatVariable PlayerHP;

    public override void Execute()
    {
        PlayerHP.Value += amount;
        OnUse?.Invoke();
    }
}