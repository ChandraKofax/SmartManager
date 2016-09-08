using System.Linq;
using DataModel;
using System.Collections.Generic;

namespace TFS.Model
{
    public class UserStory : Item
    {
        public override ItemType Type
        {
            get
            {
                return ItemType.UserStory;
            }
        }
        internal override bool HasTaskUpdateIssues()
        {
            bool hasActiveNonQATasks = false;
            if (State == TFSLiterals.StatusResolved)
            {
                Children.ForEach(c =>
                {
                    if (c.Type == ItemType.Task)
                    {
                        Task task = c as Task;
                        if (task.Activity != ActivityType.Testing)
                        {
                            if (task.Activity == ActivityType.Development)
                            {
                                string title = task.Title.ToLower();
                                if (!(title.Contains("defect") && title.Contains("poker")) ||
                                    task.RemainingWork > 0)
                                {
                                    hasActiveNonQATasks = true;
                                }
                            }
                        }
                    }
                });
            }
            if (hasActiveNonQATasks)
                return true;
            else
                return base.HasTaskUpdateIssues();
        }
    }
}
