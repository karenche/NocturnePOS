using System.ServiceModel;

namespace CardHelper
{
    [ServiceBehavior]
    public class HelperService : IHelperService
    {
        public static ulong? UID { get; set; }
        public static IdCardData IdCardData { get; set; }

        public IdCardData GetIdCardFromReader()
        {
            var result = IdCardData; IdCardData = null; return result;
        }

        public ulong? GetRfidCardFromReader()
        {
            var result = UID; UID = null; return result;
        }
    }
}
