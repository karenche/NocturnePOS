using Nocturne.BL.DTO;
using Nocturne.BL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using Nocturne.BL.Helpers;

namespace Nocturne.BL.Services
{
    public class InMemoryDiscountTypeService : IDiscountTypeService
    {
        private List<DiscountTypeDto> _types = new List<DiscountTypeDto>
        {
            new DiscountTypeDto
            {
                Id = 1,
                Name = "A modest discount",
                IsActive = true
            },
            new DiscountTypeDto
            {
                Id = 2,
                Name = "Today's special!",
                IsActive = true
            },
            new DiscountTypeDto
            {
                Id = 3,
                Name = "-50%! Everything goes!",
                IsActive = true
            },
        };

        public DiscountTypeDto[] GetAllDiscountTypes()
        {
            return _types.ToArray();
        }

        public DiscountTypeDto GetDiscountTypeById(int id)
        {
            return _types.Where(d => d.Id == id).SingleOrDefault();
        }

        public ValidationResult<int> SaveDiscountType(DiscountTypeDto type)
        {
            var validationResult = ValidateDiscountType(type);
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }

            if (type.Id > 0)
            {
                DeleteDiscountType(type.Id);
                _types.Add(type);
                
            } else
            {
                _types.Add(type);
                type.Id = _types.Count;
            }
            validationResult.Result = type.Id;
            return validationResult;
        }

        private ValidationResult<int> ValidateDiscountType(DiscountTypeDto type)
        {
            var result = new ValidationResult<int>();

            result.ValidateProperty((msg) => { return string.IsNullOrEmpty(type.Name) ? new ValidationErrorMessage(msg) : null; },
                "Name cannot be blank.",
                nameof(type.Name));

            return result;
        }

        //public void DeleteDiscountType(int id, IDiscountService discountService = null) 
        public void DeleteDiscountType(int id)
        {
            //if (discountService != null)
            //{
            //    foreach (DiscountDto d in discountService.GetDiscountByTypeId(id))
            //        discountService.DeleteDiscount(d.Id);
            //}
            _types.RemoveAll(d => d.Id == id);
        }

    }
}
