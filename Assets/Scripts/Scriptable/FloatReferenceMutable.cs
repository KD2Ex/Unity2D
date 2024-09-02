using System;

[Serializable]
public class FloatReferenceMutable
{
    public FloatVariable Variable;
    public bool UseValue;
    public float floatValue;

    public float Value
    {
        get => UseValue ? floatValue : Variable.Value; 
        set
        {
            if (UseValue) floatValue = value;
            else Variable.Value = value;
        }
    }
}