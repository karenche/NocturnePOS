namespace Nocturne.DAL
{
    public class Discount
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int DiscountTypeId { get; set; }
        public DiscountType DiscountType { get; set; }

        public int AmountPercent { get; set; }

        public bool IsActive { get; set; }
    }
}
