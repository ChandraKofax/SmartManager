using DataModel;

namespace TFS.Model
{
    public class TechnicalDebt : UserStory
    {
        public override ItemType Type
        {
            get
            {
                return ItemType.TechnicalDebt;
            }
        }
    }
}
