using System;

namespace Nocturne.BL.DTO
{
    public class PaymentDto
    {
        public int Id { get; set; }        
        public int PaymentTypeId { get; set; }
        public int SessionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }     
    }
}
