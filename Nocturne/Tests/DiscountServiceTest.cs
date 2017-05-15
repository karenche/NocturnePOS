using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Nocturne.BL.Interfaces;
using Nocturne.BL.Services;
using Nocturne.BL.DTO;

namespace Tests
{
    [TestClass]
    public class DiscountServiceTest
    {
        private IDiscountTypeService _typeService = new InMemoryDiscountTypeService();
        private IProductService _productService = new InMemoryProductService();
        private IDiscountService _discountService = new InMemoryDiscountService();

        [TestMethod]
        public void DiscountPriceIsDifferenceOfPriceAndAllDiscounts()
        {
            decimal? res = _discountService.CalculateDiscountPriceForProduct(_productService.GetProductById(1));

            Assert.AreEqual((decimal) 6.8, res);
        }

        [TestMethod]
        public void DiscountPriceIsNullWhenNoDiscounts()
        {
            decimal? res = _discountService.CalculateDiscountPriceForProduct(_productService.GetProductById(3));

            Assert.IsNull(res);
        }

        [TestMethod]
        public void DiscountPriceIs0WhenDiscountsEqualPrice()
        {
            _discountService.SaveDiscount(new DiscountDto {
                DiscountTypeId = 1,
                ProductId = 1,
                AmountPercent = 85
            });

            decimal? res = _discountService.CalculateDiscountPriceForProduct(_productService.GetProductById(1));

            Assert.AreEqual(0, res);
        }

    }
}
