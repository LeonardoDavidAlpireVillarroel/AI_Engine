using AIEngine.Decision.BehaviourTree;
using AIEngine.Decision.BehaviourTree.Tasks;
using AIEngine.Movement.Components.Algorithms;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private float detectionDistance;

    private BHT_Task tree;

    private void Awake()
    {
        PathAssignerToPathFollowingPoints pathAssigner = GetComponent<PathAssignerToPathFollowingPoints>();
        CmpPathFollowing cmpPathFollowing = GetComponent<CmpPathFollowing>();

        if (pathAssigner == null)
        {
            Debug.LogError("[Npc] No se encontr� el componente PathAssignerToPathFollowingPoints.");
            return;
        }

        tree = new BHT_Sequence
        (
            new BHT_Task[]
            {
                new EntityNear(
                    detectionDistance, 
                    gameObject
                ),
                new ChaseObjective(
                    pathAssigner,
                    cmpPathFollowing
                )
            }
        );
    }

    private void Update()
    {
        if (tree != null)
        {
            tree.Run();
        }
    }
}
