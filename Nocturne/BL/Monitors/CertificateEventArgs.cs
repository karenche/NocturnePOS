namespace Nocturne.BL.Monitors
{
    public class CertificateEventArgs
    {
        public string IdCode { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public CertificateEventArgs(string idCode, string firstName, string lastName)
        {
            IdCode = idCode;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
