
namespace TFS.Model
{
    public static class Extensions
    {
        public static double GetDoubleValue(this object doubleValue)
        {
            double value = 0;
            if (doubleValue == null)
            {
                return value;
            }
            double.TryParse(doubleValue.ToString(), out value);
            return value;
        }
    }
}
