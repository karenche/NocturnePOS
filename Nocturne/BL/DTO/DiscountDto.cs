namespace Nocturne.BL.DTO
{
    public class DiscountDto
    {
        public int Id { get; set; }
        public int DiscountTypeId { get; set; }
        public int ProductId { get; set; }
        public int AmountPercent { get; set; }
    }
}
