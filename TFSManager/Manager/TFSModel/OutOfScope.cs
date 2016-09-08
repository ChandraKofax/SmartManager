using DataModel;

namespace TFS.Model
{
    public class OutOfScope : UserStory
    {
        public override ItemType Type
        {
            get
            {
                return ItemType.OutOfScope;
            }
        }
    }
}
