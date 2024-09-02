using UnityEngine;

[CreateAssetMenu]
public class StatsParticle : ScriptableObject, IStatsVisitor
{
    public float health;
    public float mana;
    
    public void Visit(HealthComponent health)
    {
        health.Add(this.health);
        Debug.Log("health visited");
    }

    public void Visit(ManaComponent mana)
    {
        mana.Value += this.mana;
        Debug.Log("mana visited");
    }
}
