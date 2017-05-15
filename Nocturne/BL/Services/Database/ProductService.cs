using Nocturne.BL.Interfaces;
using System.Linq;
using Nocturne.BL.DTO;
using Nocturne.BL.Helpers;
using Nocturne.DAL;

namespace Nocturne.BL.Services
{
    public class ProductService : IProductService
    {
        public void DeleteProduct(int id)
        {
            using (var dc = new NocturneContext())
            {
                var product = dc.Products.Single(q => q.Id == id);
                product.IsActive = false;
                dc.SaveChanges();
            }
        }

        public ProductDto[] GetAllProducts()
        {
            using (var dc = new NocturneContext())
            {
                return SelectProduct(dc.Products.Where(q=> q.IsActive)).ToArray();
            }
        }

        public ProductDto GetProductById(int id)
        {
            using (var dc = new NocturneContext())
            {
                return SelectProduct(dc.Products).Where(p => p.Id == id).SingleOrDefault();
            }
        }

        public ValidationResult<int> SaveProduct(ProductDto productDto)
        {
            var validationResult = ValidateProduct(productDto);
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }
            using (var dc = new NocturneContext())
            {
                Product productDb;
                if (productDto.Id > 0)
                {
                    productDb = dc.Products.Single(q => q.Id == productDto.Id);
                }
                else
                {
                    productDb = new Product();
                    productDb.IsActive = true;
                    dc.Products.Add(productDb);
                }

                productDb.Name = productDto.Name;
                productDb.Description = productDto.Description;
                productDb.Price = productDto.Price;
                productDb.DisplayImage = productDto.DisplayImage;

                dc.SaveChanges();
                productDto.Id = productDb.Id;
                validationResult.Result = productDto.Id;
            }                                        
            return validationResult;
        }

        private ValidationResult<int> ValidateProduct(ProductDto product)
        {
            const string emptyErrorTemplate = "{0} cannot be blank.";
            var result = new ValidationResult<int>();

            result.ValidateProperty((msg) => { return string.IsNullOrEmpty(product.Description) || product.Description.Trim().Length == 0 ? new ValidationErrorMessage(msg) : null; },
                string.Format(emptyErrorTemplate, "Description"),
                nameof(product.Description));

            result.ValidateProperty((msg) => { return string.IsNullOrEmpty(product.Name) || product.Name.Trim().Length == 0 ? new ValidationErrorMessage(msg) : null; },
               string.Format(emptyErrorTemplate, "Name"),
               nameof(product.Name));

            result.ValidateProperty((msg) => { return product.Price < 0 ? new ValidationErrorMessage(msg) : null; },
                "Product price must be 0 or greater",
               nameof(product.Price));

            return result;
        }

        private IQueryable<ProductDto> SelectProduct(IQueryable<Product> query)
        {
            return query.Select(u => new ProductDto
            {
                Description = u.Description,
                Id = u.Id,
                Name = u.Name,
                Price = u.Price,
                DisplayImage = u.DisplayImage
            });
        }    
    }
}
