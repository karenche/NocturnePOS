using System;

namespace Nocturne.BL.DTO
{
    public class UsedProductDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SessionId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }   
        public int RegisteredBy { get; set; }
    }
}
