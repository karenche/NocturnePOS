using Nocturne.BL.DTO;
using Nocturne.BL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using Nocturne.BL.Helpers;

namespace Nocturne.BL.Services
{
    public class InMemoryDiscountService : IDiscountService
    {
        private List<DiscountDto> _discounts = new List<DiscountDto>
        {
            new DiscountDto
            {
                Id = 1,
                DiscountTypeId = 1,
                ProductId = 1,
                AmountPercent = 10
            },
            new DiscountDto
            {
                Id = 2,
                DiscountTypeId = 2,
                ProductId = 1,
                AmountPercent = 5
            },
            new DiscountDto
            {
                Id = 3,
                DiscountTypeId = 3,
                ProductId = 2,
                AmountPercent = 50
            }
        };

        public decimal? CalculateDiscountPriceForProduct(ProductDto product)
        {
            decimal res = 0;

            res = _discounts.Where(d => d.ProductId == product.Id)
                    //.Where(typeService.GetDiscountTypeById(d.DiscountTypeId).IsActive)
                    .Sum(d => d.AmountPercent) * product.Price / 100;

            if (res >= product.Price && product.Price != 0) return 0;
            else if (res > 0) return product.Price - res;

            return null;
        }

        public void DeleteDiscount(int id)
        {
            _discounts.RemoveAll(d => d.Id == id);
        }

        public DiscountDto GetDiscountById(int id)
        {
            return _discounts.Where(d => d.Id == id).SingleOrDefault();
        }

        public DiscountDto[] GetDiscountByProductId(int id)
        {
            return _discounts.Where(d => d.ProductId == id).ToArray();
        }

        public DiscountDto[] GetDiscountByTypeId(int id)
        {
            return _discounts.Where(d => d.DiscountTypeId == id).ToArray();
        }

        public DiscountDto[] GetAllDiscounts()
        {
            return _discounts.ToArray();
        }

        public ValidationResult<int> SaveDiscount(DiscountDto discount)
        {
            var validationResult = ValidateDiscount(discount);
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }

            if (discount.Id > 0)
            {
                DeleteDiscount(discount.Id);
                _discounts.Add(discount);
            }
            else
            {
                _discounts.Add(discount);
                discount.Id = _discounts.Count;
            }

            validationResult.Result = discount.Id;
            return validationResult;
        }

        private int GetDiscountPercentForProduct(int productId, int discountId)
        {
            return _discounts.Where(d => d.ProductId == productId && d.Id != discountId).Sum(d => d.AmountPercent);
        }

        private ValidationResult<int> ValidateDiscount(DiscountDto discount)
        {
            var result = new ValidationResult<int>();

            result.ValidateProperty(
                    (msg) =>
                    {
                        return discount.AmountPercent + GetDiscountPercentForProduct(discount.ProductId, discount.Id) > 100 ?
                                new ValidationErrorMessage(msg) : null;
                    },
                    "Total discount percent for product can't exceed 100.",
                    nameof(discount.AmountPercent)
                    );

            result.ValidateProperty((msg) => { return discount.AmountPercent < 0 || discount.AmountPercent > 100 ? new ValidationErrorMessage(msg) : null; },
                "Amount percent must be between 0 and 100.",
                nameof(discount.AmountPercent));

            result.ValidateProperty((msg) => { return discount.DiscountTypeId == 0 ? new ValidationErrorMessage(msg) : null; },
                "You must select a discount type.",
                nameof(discount.DiscountTypeId));

            result.ValidateProperty((msg) => { return discount.ProductId == 0 ? new ValidationErrorMessage(msg) : null; },
                "You must select a product to discount.",
                nameof(discount.ProductId));

            return result;
        }
    }
}
