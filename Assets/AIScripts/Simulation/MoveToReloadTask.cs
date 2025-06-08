//using AIEngine.Decision.BehaviourTree;
//using AIEngine.Movement.Components.Algorithms;
//using UnityEngine;
//using System;

//public class MoveToReloadTask : BHT_Task
//{
//    private readonly PathAssignerToPathFollowingPoints pathAssigner;
//    private readonly CmpPathFollowing pathFollowing;
//    private readonly Func<Vector3> getReloadPosition;
//    private readonly Action onReloadComplete;

//    private const float reachThreshold = 4f;

//    public MoveToReloadTask(
//        PathAssignerToPathFollowingPoints pathAssigner,
//        CmpPathFollowing pathFollowing,
//        Func<Vector3> getReloadPosition,
//        Action onReloadComplete)
//    {
//        this.pathAssigner = pathAssigner;
//        this.pathFollowing = pathFollowing;
//        this.getReloadPosition = getReloadPosition;
//        this.onReloadComplete = onReloadComplete;
//    }

//    public override bool Run()
//    {
//        Vector3 currentReloadPosition = getReloadPosition();
//        float distance = Vector3.Distance(pathFollowing.transform.position, currentReloadPosition);

//        if (distance <= reachThreshold)
//        {
//            pathFollowing.StopFollowing();
//            onReloadComplete?.Invoke();
//            return true;
//        }

//        // Actualiza la ruta al nuevo punto
//        pathAssigner.AssignPathToPosition(currentReloadPosition);
//        pathFollowing.UpdatePath(pathAssigner.pathFollowing);
//        return false;
//    }
//}
using UnityEngine;
using System.Linq;
using AIEngine.Decision.BehaviourTree;
using AIEngine.Movement.Components.Agents;
using AIEngine.Movement.Components.Algorithms;

public class MoveToReloadTask : BHT_Task
{
    private readonly PathAssignerToPathFollowingPoints pathAssigner;
    private readonly CmpPathFollowing cmpPathFollowing;
    private readonly System.Action onReloadComplete;

    private const float arriveThreshold = 0.1f;

    public MoveToReloadTask(PathAssignerToPathFollowingPoints pathAssigner, CmpPathFollowing cmpPathFollowing, System.Action onReloadComplete)
    {
        this.pathAssigner = pathAssigner;
        this.cmpPathFollowing = cmpPathFollowing;
        this.onReloadComplete = onReloadComplete;
    }

    public override bool Run()
    {
        GameObject closest = FindClosestReloadPoint();

        if (closest == null)
            return false;

        Vector3 reloadPos = closest.transform.position;
        float dist = Vector3.Distance(cmpPathFollowing.transform.position, reloadPos);

        if (dist <= arriveThreshold)
        {
            cmpPathFollowing.StopFollowing();
            onReloadComplete?.Invoke();
            return true;
        }
        else
        {
            pathAssigner.AssignPathToPosition(reloadPos);
            cmpPathFollowing.UpdatePath(pathAssigner.pathFollowing);
            return false;
        }
    }

    private GameObject FindClosestReloadPoint()
    {
        var all = GameObject.FindGameObjectsWithTag("Ammo")
            .Where(obj => obj.GetComponent<CmpStatic>() != null);

        if (!all.Any()) return null;

        return all.OrderBy(obj => Vector3.Distance(cmpPathFollowing.transform.position, obj.transform.position)).First();
    }
}
