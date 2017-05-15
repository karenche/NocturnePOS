using System;
using System.Collections.Generic;
using Nocturne.BL.DTO;
using System.Linq;
using Nocturne.BL.Interfaces;
using Nocturne.BL.Helpers;

namespace Nocturne.BL.Services
{
    public class InMemoryPaymentService : IPaymentService
    {
        private readonly List<PaymentDto> _payments = new List<PaymentDto>
        {
            new PaymentDto
            {
                Id = 1,
                Amount = 2,
                PaymentTypeId = 1,
                SessionId = 1,
                Time = new DateTime(2015,12,13)
            }
        };

        private readonly List<PaymentTypeDto> _paymentTypes = new List<PaymentTypeDto>
        {
            new PaymentTypeDto
            {
                Id = 1,
                Name = "Cash"
            }
        };

        public PaymentDto[] Find(Func<PaymentDto, bool> predicate)
        {
            return _payments.Where(predicate).ToArray();
        }

        public PaymentDto GetPayment(int id)
        {
            return _payments.Where(q => q.Id == id).SingleOrDefault();
        }

        public ValidationResult<int> SavePayment(PaymentDto paymentDto)
        {
            var validationResult = ValidatePayment(paymentDto);
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }

            if (paymentDto.Id > 0)
            {
                _payments.RemoveAll(u => u.Id == paymentDto.Id);
                _payments.Add(paymentDto);
                validationResult.Result = paymentDto.Id;
            }
            else
            {
                _payments.Add(paymentDto);
                paymentDto.Id = _payments.Count;
                validationResult.Result = paymentDto.Id;
            }
            return validationResult;
        }

        public PaymentTypeDto[] Find(Func<PaymentTypeDto, bool> predicate)
        {
            return _paymentTypes.Where(predicate).ToArray();
        }

        public PaymentTypeDto GetPaymentTypeDto(int id)
        {
            return _paymentTypes.Where(q => q.Id == id).SingleOrDefault();
        }

        private ValidationResult<int> ValidatePayment(PaymentDto paymentDto)
        {
            const string emptyErrorTemplate = "{0} can not be blank.";
            const string atLeastOneErrorTemplate = "{0} must be >= 1.";
            var result = new ValidationResult<int>();

            result.ValidateProperty((msg) => { return paymentDto.PaymentTypeId <= 0 ? new ValidationErrorMessage(msg) : null; },
                string.Format(emptyErrorTemplate, "PaymentType"),
                nameof(paymentDto.PaymentTypeId));

            result.ValidateProperty((msg) => { return paymentDto.Amount < decimal.One ? new ValidationErrorMessage(msg) : null; },
               string.Format(atLeastOneErrorTemplate, "Amount"),
               nameof(paymentDto.Amount));

            result.ValidateProperty((msg) => { return paymentDto.SessionId <= 0 ? new ValidationErrorMessage(msg) : null; },
               string.Format(emptyErrorTemplate, "Session"),
               nameof(paymentDto.SessionId));

            return result;
        }
    }
}
