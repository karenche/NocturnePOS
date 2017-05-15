using Nocturne.BL.DTO;
using Nocturne.BL.Interfaces;
using System.Linq;
using Nocturne.BL.Helpers;
using Nocturne.DAL;

namespace Nocturne.BL.Services
{
    public class DiscountTypeService : IDiscountTypeService
    {
        public DiscountTypeDto[] GetAllDiscountTypes()
        {
            using (var dc = new NocturneContext())
            {
                return SelectDiscountType(dc.DiscountTypes.Where(q => q.IsActive)).ToArray();
            }
        }

        public DiscountTypeDto GetDiscountTypeById(int id)
        {
            using (var dc = new NocturneContext())
            {
                return SelectDiscountType(dc.DiscountTypes).Where(p => p.Id == id).SingleOrDefault();
            }
        }

        public ValidationResult<int> SaveDiscountType(DiscountTypeDto typeDto)
        {
            var validationResult = ValidateDiscountType(typeDto);
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }

            using (var dc = new NocturneContext())
            {
                DiscountType typeDb;
                if (typeDto.Id > 0)
                {
                    typeDb = dc.DiscountTypes.Single(q => q.Id == typeDto.Id);
                }
                else
                {
                    typeDb = new DiscountType();   
                    dc.DiscountTypes.Add(typeDb);
                }

                typeDb.Name = typeDto.Name;
                typeDb.IsActive = typeDto.IsActive;

                dc.SaveChanges();
                typeDto.Id = typeDb.Id;
                validationResult.Result = typeDto.Id;
            }

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

        public void DeleteDiscountType(int id)
        {
            using (var dc = new NocturneContext())
            {
                var discountType = dc.DiscountTypes.Single(q => q.Id == id);
                discountType.IsActive = false;
                dc.SaveChanges();
            }
        }

        private IQueryable<DiscountTypeDto> SelectDiscountType(IQueryable<DiscountType> query)
        {
            return query.Select(u => new DiscountTypeDto
            {
                IsActive = u.IsActive,
                Id = u.Id,
                Name = u.Name
            });
        }
    }
}
