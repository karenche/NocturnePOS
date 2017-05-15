using System;
namespace Nocturne.DAL
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }

        public int RegisteredById { get; set; }
        public User RegisteredBy { get; set; }

        public int? ClientId { get; set; }
        public Client Client { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}
