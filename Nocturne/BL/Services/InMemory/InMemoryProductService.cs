using Nocturne.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nocturne.BL.DTO;
using Nocturne.BL.Helpers;

namespace Nocturne.BL.Services
{
    public class InMemoryProductService : IProductService
    {
        private List<ProductDto> _products = new List<ProductDto>
        {
            new ProductDto
            {
                Id = 1,
                Name = "Caesar Salad",
                Description = "Romaine with freshly made croutons, parmesan cheese, and our great caesar dressing",
                DisplayImage = null,
                Price = 8
            },
            new ProductDto
            {
                Id = 2,
                Name = "Whole Chicken",
                Description = "Total dinner with 3 sides.",
                DisplayImage = null,
                Price = 27
            },
            new ProductDto
            {
                Id = 3,
                Name = "Chicken Ceasar Salad Wrap",
                Description = "In whole wheat tortilla.",
                DisplayImage = null,
                Price = 12
            },
            new ProductDto
            {
                Id = 4,
                Name = "Beef Chili",
                Description = "Chili con carne topped with cheese, served with rice and our fresh chips.",
                DisplayImage = null,
                Price = 14
            },
            new ProductDto
            {
                Id = 5,
                Name = "Tossed Salad",
                Description = "Tossed salad with chips and taco vinaigrette.",
                DisplayImage = null,
                Price = 9
            },
            new ProductDto
            {
                Id = 6,
                Name = "Mineral water",
                Description = "Exceptionally good carbonized water",
                DisplayImage = null,
                Price = 2
            }
        };

        //public int DeleteProduct(int id, IDiscountService discountService = null)
        public void DeleteProduct(int id)
        {
            //if (discountService != null)
            //{
            //    foreach (DiscountDto d in discountService.GetDiscountByProductId(id))
            //        discountService.DeleteDiscount(d.Id);

            //}
            //return _products.RemoveAll(p => p.Id == id);   
            _products.RemoveAll(p => p.Id == id);
        }

        public ProductDto[] GetAllProducts()
        {
            return _products.ToArray();
        }

        public ProductDto GetProductById(int id)
        {
            return _products.Where(p => p.Id == id).SingleOrDefault();
        }

        public ValidationResult<int> SaveProduct(ProductDto product)
        {
            var validationResult = ValidateProduct(product);
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }
            if (product.Id > 0)
            {
                _products.RemoveAll(p => p.Id == product.Id);
                _products.Add(product);
            }
            else
            {
                _products.Add(product);
                product.Id = _products.Count;
            }
            validationResult.Result = product.Id;
            return validationResult;
        }

        private ValidationResult<int> ValidateProduct(ProductDto product)
        {
            const string emptyErrorTemplate = "{0} cannot be blank.";
            var result = new ValidationResult<int>();

            result.ValidateProperty((msg) => { return string.IsNullOrEmpty(product.Description) || product.Description.Trim().Length == 0 ? new ValidationErrorMessage(msg) : null; },
                string.Format(emptyErrorTemplate, "Description"),
                nameof(product.Description));

            result.ValidateProperty((msg) => { return string.IsNullOrEmpty(product.Name) || product.Name.Trim().Length == 0? new ValidationErrorMessage(msg) : null; },
               string.Format(emptyErrorTemplate, "Name"),
               nameof(product.Name));

            result.ValidateProperty((msg) => { return product.Price < 0 ? new ValidationErrorMessage(msg) : null; },
                "Product price must be 0 or greater",
               nameof(product.Price));

            return result;
        }

    }
}
