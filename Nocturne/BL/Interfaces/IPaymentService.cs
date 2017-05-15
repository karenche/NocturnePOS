using System;
using Nocturne.BL.DTO;
using Nocturne.BL.Helpers;

namespace Nocturne.BL.Interfaces
{
    /// <summary>
    /// Service to manage payments.
    /// </summary>
    public interface IPaymentService
    {
        PaymentDto[] Find(Func<PaymentDto, bool> predicate);
        PaymentDto GetPayment(int id);
        ValidationResult<int> SavePayment(PaymentDto paymentDto);  
        PaymentTypeDto[] Find(Func<PaymentTypeDto, bool> predicate);
        PaymentTypeDto GetPaymentTypeDto(int id);
    }
}