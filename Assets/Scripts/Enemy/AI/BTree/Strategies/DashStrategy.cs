using UnityEngine;

namespace AI.Btree
{
    public class DashStrategy : IStrategy
    {
        private DashNavMesh dash;
        private Transform origin;
        private Sensor sensor;

        public DashStrategy(DashNavMesh dash, Transform origin, Sensor sensor)
        {
            this.dash = dash;
            this.origin = origin;
            this.sensor = sensor;
        }

        public Node.Status Process()
        {
            var direction = (origin.transform.position - sensor.TargetTransform.position).normalized;
            dash.Execute(direction);
            return Node.Status.Success;
        }
    }
}