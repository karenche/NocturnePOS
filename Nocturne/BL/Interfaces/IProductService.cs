using Nocturne.BL.DTO;
using Nocturne.BL.Helpers;

namespace Nocturne.BL.Interfaces
{
    /// <summary>
    /// Service interface to manage products.
    /// </summary>
    public interface IProductService
    {
        ProductDto GetProductById(int id);
        ProductDto[] GetAllProducts();
        ValidationResult<int> SaveProduct(ProductDto product);
        void DeleteProduct(int id);
    }
}
