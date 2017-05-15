using Nocturne.BL.Helpers;

namespace Nocturne.BL.DTO
{
    public class CardDto
    {
        public int Id { get; set; }
        public CardTypeEnum CardType { get; set; }
        public string DisplayName
        {
            get
            {
                return RegCard ?? Uid.ToString();
            }
        }
        public string Firstname { get; set;  } // used only for ID-card
        public string Lastname { get; set;  } // used only for ID-card
        public string RegCard { get; set; }
        public ulong Uid { get; set; }
    }
}
