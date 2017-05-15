using System.ServiceModel;

namespace CardHelper
{
    [ServiceContract]
    public interface IHelperService
    {
        [OperationContract]
        IdCardData GetIdCardFromReader();

        [OperationContract]
        ulong? GetRfidCardFromReader();
    }
}
