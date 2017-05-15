namespace Nocturne.App.Helpers
{
    public interface ISettngs
    {
        bool InRfidSupported();
        string RfdComPort();
        bool InIdCardSupported();
    }
}
