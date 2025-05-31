using System;
using System.Collections.Generic;
using AIEngine.Decision.Experts;

namespace AIEngine.Decision.Blackboard
{
    public class Blackboard
    {
        public List<BlackboardData> entries = new();

        public List<Expert> experts = new();

        public Action[] actionsToExecute;

        public BlackboardData GetDataByKey(string key)
        {
            foreach (var entry in entries)
            {
                if (entry.key == key)
                {
                    return entry;
                }
            }
            return null;
        }

        public Action[] Update()
        {
            float maxInsistence = float.MinValue;
            Expert bestExpert = null;

            foreach (var expert in experts)
            {
                float insistence = expert.GetInsistence(this);
                if (insistence > maxInsistence)
                {
                    maxInsistence = insistence;
                    bestExpert = expert;
                }
            }

            return bestExpert?.Run(this);
        }
    }
}
