using UnityEngine;

namespace AI.Btree
{
    public class AbilityStrategy : IStrategy
    {
        private Sensor sensor;
        private Transform origin;
        
        //ability
        private DirectionalWeapon weapon;

        public AbilityStrategy(Transform origin, Sensor sensor, DirectionalWeapon weapon)
        {
            this.origin = origin;
            this.sensor = sensor;
            this.weapon = weapon;
        }
        
        public Node.Status Process()
        {
            var direction = (sensor.TargetTransform.position - origin.position).normalized;
            weapon.Attack(direction, .5f);

            return Node.Status.Running;
        }

        public void Reset()
        {
            
        }
    }
}