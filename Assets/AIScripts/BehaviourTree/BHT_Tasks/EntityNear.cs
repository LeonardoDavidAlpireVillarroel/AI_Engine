using AIEngine.Movement.Components.Agents;
using UnityEngine;

namespace AIEngine.Decision.BehaviourTree.Tasks
{
    public class EntityNear : BHT_Task
    {
        private readonly float detectionRadius;
        private readonly GameObject myself;

        public GameObject Target { get; private set; }

        public EntityNear(float detectionRadius, GameObject myself)
        {
            this.detectionRadius = detectionRadius;
            this.myself = myself;
        }

        public override bool Run()
        {
            Target = null;
            CmpStatic[] allStatics = GameObject.FindObjectsByType<CmpStatic>(FindObjectsSortMode.None);
            float closestDistance = float.MaxValue;

            foreach (var cmp in allStatics)
            {
                GameObject candidate = cmp.gameObject;

                // Evitar detectarse a sí mismo
                if (candidate == myself) continue;

                // Verificar si tiene el tag adecuado
                if (!candidate.CompareTag("Player")) continue;

                float dist = Vector3.Distance(myself.transform.position, candidate.transform.position);
                if (dist < detectionRadius && dist < closestDistance)
                {
                    Target = candidate;
                    closestDistance = dist;
                }
            }

            return Target != null;
        }
    }
}

