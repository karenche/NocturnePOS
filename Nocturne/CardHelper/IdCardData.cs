using System.Runtime.Serialization;

namespace CardHelper
{
    [DataContract]
    public class IdCardData
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string IdCode { get; set; }
        [DataMember]
        public string LastName { get; set; }

        public IdCardData()
        {

        }

        public IdCardData(string idCode, string firstName, string lastName)
        {
            IdCode = idCode;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
