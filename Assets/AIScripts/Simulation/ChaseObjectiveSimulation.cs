using AIEngine.Decision.BehaviourTree;
using AIEngine.Movement.Components.Algorithms;
using UnityEngine;

namespace AIEngine.Decision.BehaviourTree.Tasks
{
    public class ChaseObjectiveSimulation : BHT_Task
    {
        private readonly PathAssignerToPathFollowingPoints pathAssigner;
        private readonly CmpPathFollowing cmpPathFollowing;
        private readonly EntityNearSimulation detector;
        private readonly float stopDistance;

        public ChaseObjectiveSimulation(PathAssignerToPathFollowingPoints pathAssigner, CmpPathFollowing cmpPathFollowing, EntityNearSimulation detector, float stopDistance = 2f)
        {
            this.pathAssigner = pathAssigner;
            this.cmpPathFollowing = cmpPathFollowing;
            this.detector = detector;
            this.stopDistance = stopDistance;
        }

        public override bool Run()
        {
            if (pathAssigner == null || cmpPathFollowing == null || detector == null)
            {
                return false;
            }

            if (detector.Target != null && detector.TargetDistance <= stopDistance)
            {
                cmpPathFollowing.StopFollowing();
                return true;
            }

            pathAssigner.AssignPath(detector.Target.transform.position); // Asegurate que `AssignPath(Vector3)` exista
            return true;
        }

        public void Stop()
        {
            cmpPathFollowing.StopFollowing();
        }
    }
}
