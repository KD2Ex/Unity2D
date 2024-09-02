using UnityEngine;

namespace Structs
{
    public struct DamageMessage
    {
        public DamageMessage(
            float value, 
            float stunValue, 
            float knockbackForce, 
            Vector2 knockbackDirection)
        {
            Value = value;
            StunValue = stunValue;
            KnockbackForce = knockbackForce;
            KnockbackDirection = knockbackDirection;
        }

        public float Value { get; }
        public float StunValue { get; }
        public float KnockbackForce { get; }
        public Vector2 KnockbackDirection { get; }
    }
}