using UnityEngine;

[CreateAssetMenu]
public class DashStats : ScriptableObject
{
    public FloatReference force;
    public FloatReference cooldown;
    public FloatReference dashTime;
    public AnimationCurve curve;
}
