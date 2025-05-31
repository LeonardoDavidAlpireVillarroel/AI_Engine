using AIEngine.Decision.BehaviourTree;
using AIEngine.Movement.Components.Algorithms;

public class ChaseObjective : BHT_Task
{
    private readonly PathAssignerToPathFollowingPoints pathAssigner;
    private readonly CmpPathFollowing cmpPathFollowing;

    public ChaseObjective(PathAssignerToPathFollowingPoints pathAssigner, CmpPathFollowing cmpPathFollowing)
    {
        this.pathAssigner = pathAssigner;
        this.cmpPathFollowing = cmpPathFollowing;
    }

    public override bool Run()
    {
        if (pathAssigner == null || cmpPathFollowing == null)
        {
            return false;
        }

        pathAssigner.AssignPath();
        return true;
    }

    public void Stop()
    {
        cmpPathFollowing.StopFollowing();
    }
}

