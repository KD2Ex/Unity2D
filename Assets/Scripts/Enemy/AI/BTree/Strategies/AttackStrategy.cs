using MEC;
using UnityEngine;

namespace AI.Btree
{
    public class AttackStrategy : IStrategy
    {
        private MeleeWeapon weapon;
        private Rigidbody2D origin;
        private Sensor sensor;
        private bool started;

        public AttackStrategy(MeleeWeapon weapon, Rigidbody2D origin, Sensor sensor)
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
                var direction = ((Vector2)sensor.TargetTransform.position - origin.position).normalized;
                weapon.Attack(direction, 0f);
                
                /*
                Timing.RunCoroutine(
                    MoveToPoint
                        .ExecuteTiming(origin, direction, 10f, .3f)
                    );
                    */
                
                return Node.Status.Running;
            }

            return weapon.Attacking ? Node.Status.Running : Node.Status.Success;
        }
        
    }
}