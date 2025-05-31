using System;

namespace AIEngine.Decision.Experts
{
    public abstract class Expert
    {
        public abstract float GetInsistence(Blackboard.Blackboard blackboard);
        public abstract Action[] Run(Blackboard.Blackboard blackboard);
    }
}
