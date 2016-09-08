using DataModel;

namespace TFS.Model
{
    public class Feature : UserStory
    {
        public override ItemType Type
        {
            get
            {
                return ItemType.Feature;
            }
        }
    }
}
