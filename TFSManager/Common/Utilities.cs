using DataModel;
using System;

namespace TFS.Common
{
    public static class Utilities
    {
        public static ActivityType GetActityType(string activityType)
        {
            ActivityType type = ActivityType.Development;
            Enum.TryParse<ActivityType>(activityType, out type);
            return type;
        }

        public static string GetEffortString(double totalBurn, double devBurn, double QABurn, double TWBurn)
        {
            string effortString = "{0} ";
            string burnPartString = string.Empty;
            bool needSpace = false;

            if (totalBurn > 0)
            {
                effortString += "({" + "1" + "})";
                if (devBurn > 0)
                {
                    burnPartString = "Dev:" + devBurn.ToString();
                    needSpace = true;
                }
                if (QABurn > 0)
                {
                    if (needSpace) { burnPartString += " "; }
                    burnPartString += "QA:" + QABurn.ToString();
                    needSpace = true;
                }
                if (TWBurn > 0)
                {
                    if (needSpace) { burnPartString += " "; }
                    burnPartString += "TW:" + TWBurn.ToString();
                }
            }

            return string.Format(effortString, totalBurn.ToString(), burnPartString);
        }
    }
}
