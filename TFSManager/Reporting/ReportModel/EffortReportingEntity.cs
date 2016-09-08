using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFS.Reporting
{
    public class EffortReportingEntity
    {
        public double Effort { get; set; }
        public double DevEffort { get; set; }
        public double QAEffort { get; set; }
        public double TWEffort { get; set; }
        public EffortReportingEntity()
        {
            Effort = 0;
            DevEffort = 0;
            QAEffort = 0;
            TWEffort = 0;
        }

        public override string ToString()
        {
            string effortString = "{0} ";
            string burnPartString = string.Empty;
            bool needSpace = false;

            if (Effort > 0)
            {
                effortString += "({" + "1" + "})";
                if (DevEffort > 0)
                {
                    burnPartString = "Dev:" + DevEffort.ToString();
                    needSpace = true;
                }
                if (QAEffort > 0)
                {
                    if (needSpace) { burnPartString += " "; }
                    burnPartString += "QA:" + QAEffort.ToString();
                    needSpace = true;
                }
                if (TWEffort > 0)
                {
                    if (needSpace) { burnPartString += " "; }
                    burnPartString += "TW:" + TWEffort.ToString();
                }
            }

            return string.Format(effortString, Effort.ToString(), burnPartString);
        }
    }
}
