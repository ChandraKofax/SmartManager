using DataModel;

namespace TFS.Model
{
    public class EnhancementRequest : UserStory
    {
        public override ItemType Type
        {
            get
            {
                return ItemType.EnhancementRequest;
            }
        }
    }
}
