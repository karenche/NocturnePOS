using System;

namespace Nocturne.DAL
{
    public class UsedProduct
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        public int RegisteredById { get; set; }
        public User RegisteredBy { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }
    }
}
