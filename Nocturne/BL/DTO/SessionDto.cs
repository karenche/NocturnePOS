using System;

namespace Nocturne.BL.DTO
{
    public class SessionDto
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public int CardId { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }     
        public int RegisteredBy { get; set; }
    }
}
