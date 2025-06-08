using AIEngine.Decision.BehaviourTree;

public class CheckAmmoTask : BHT_Task
{
    private readonly System.Func<bool> condition;

    public CheckAmmoTask(System.Func<bool> condition)
    {
        this.condition = condition;
    }

    public override bool Run()
    {
        return condition();
    }
}
