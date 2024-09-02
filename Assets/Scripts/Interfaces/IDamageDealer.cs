namespace Interfaces
{
    public interface IDamageDealer
    {
        public float Damage { get; protected set; }
        public float Stun { get; protected set; }
        public float Knockback { get; protected set; }
        
    }
}