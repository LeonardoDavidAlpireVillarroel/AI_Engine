using AIEngine.Movement.Components.Agents;
using UnityEngine;

namespace AIEngine.Decision.BehaviourTree.Tasks
{
    public class EntityNearSimulation : BHT_Task
    {
        private readonly float detectionRadius;
        private readonly GameObject myself;

        public GameObject Target { get; private set; }
        public float TargetDistance { get; private set; }

        public EntityNearSimulation(float detectionRadius, GameObject myself)
        {
            this.detectionRadius = detectionRadius;
            this.myself = myself;
        }

        public override bool Run()
        {
            Target = null;
            TargetDistance = float.MaxValue;

            CmpStatic[] allStatics = GameObject.FindObjectsByType<CmpStatic>(FindObjectsSortMode.None);

            foreach (var cmp in allStatics)
            {
                GameObject candidate = cmp.gameObject;

                if (candidate == myself) continue;
                if (!candidate.CompareTag("Player")) continue;

                float dist = Vector3.Distance(myself.transform.position, candidate.transform.position);
                if (dist < detectionRadius && dist < TargetDistance)
                {
                    Target = candidate;
                    TargetDistance = dist;
                }
            }

            return Target != null;
        }
    }
}
