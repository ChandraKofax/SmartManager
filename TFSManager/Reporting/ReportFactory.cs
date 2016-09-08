using TFS.Model;

namespace TFS.Reporting
{
    public class ReportFactory
    {
        public static Item CreateItem(WorkItemNode workItem)
        {
            Item item = null;
            switch ((string)workItem.Item.Fields[TFSLiterals.WorkItemType].Value)
            {
                case TFSLiterals.Task:
                    item = new TFS.Model.Task();
                    break;
                case TFSLiterals.TechDebt:
                    item = new TFS.Model.TechnicalDebt();
                    break;
                case TFSLiterals.UserStory:
                    item = new TFS.Model.UserStory();
                    break;
                case TFSLiterals.OutOfScope:
                    item = new TFS.Model.OutOfScope();
                    break;
                case TFSLiterals.Bug:
                    item = new TFS.Model.Bug();
                    break;
                case TFSLiterals.Feature:
                    item = new TFS.Model.Feature();
                    break;
                case TFSLiterals.EnhancementRequest:
                    item = new TFS.Model.EnhancementRequest();
                    break;
                default:
                    item = new TFS.Model.Item();
                    break;
            }

            item.Initialize(workItem);
            return item;
        }
    }
}
