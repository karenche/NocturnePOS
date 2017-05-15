using Nocturne.BL.DTO;
using Nocturne.BL.Helpers;

namespace Nocturne.BL.Interfaces
{
    /// <summary>
    /// Service interface to manage discount types.
    /// </summary>
    public interface IDiscountTypeService
    {
        DiscountTypeDto GetDiscountTypeById(int id);
        DiscountTypeDto[] GetAllDiscountTypes();
        ValidationResult<int> SaveDiscountType(DiscountTypeDto discount);
        void DeleteDiscountType(int id);
    }
}
