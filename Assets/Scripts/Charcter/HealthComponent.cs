using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class HealthComponent : MonoBehaviour, IVisitable
{
    [SerializeField] private bool resetHP;
    
    [SerializeField] private FloatReferenceMutable CurrentHp;
    [SerializeField] private FloatReferenceMutable MaxHp;

    public float Value => CurrentHp.Value;
    public float MaxValue => MaxHp.Value;
    
    private void Awake()
    {
        CurrentHp.Value = resetHP ? MaxHp.Value : CurrentHp.Value;
    }

    private void Start()
    {
    }

    private void InstantiateScriptable()
    {
        if (CurrentHp.UseValue) return;
        
        var instance = ScriptableObject.CreateInstance<FloatVariable>();
        instance.Value = CurrentHp.Value;

        CurrentHp.Variable = instance;
    }
    
    public void Accept(IVisitor visitor)
    {
        
    }

    public void Accept(IStatsVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Add(float value)
    {
        if (Value + value > MaxValue)
        {
            CurrentHp.Value = MaxHp.Value;
        }
        else
        {
            CurrentHp.Value += value;
        }
    }
    
}


