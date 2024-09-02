using UnityEngine;

namespace AI.Btree
{
    public class AttackStrategy : IStrategy
    {
        private MeleeWeapon weapon;
        private Transform origin;
        private Sensor sensor;
        private bool started;

        public AttackStrategy(MeleeWeapon weapon, Transform origin, Sensor sensor)
        {
            this.weapon = weapon;
            this.origin = origin;
            this.sensor = sensor;
        }

        public Node.Status Process()
        {
            if (!sensor.TargetTransform) return Node.Status.Failure;

            //Debug.Log(weapon.Attacking);
            
            if (!started)
            {
                var direction = (sensor.TargetTransform.position - origin.position).normalized;
                weapon.Attack(direction, 0f);
                return Node.Status.Running;
            }

            return weapon.Attacking ? Node.Status.Running : Node.Status.Success;
        }
        
        
    }
}