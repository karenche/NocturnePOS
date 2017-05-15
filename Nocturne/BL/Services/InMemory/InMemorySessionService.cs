using System;
using System.Collections.Generic;
using Nocturne.BL.DTO;
using Nocturne.BL.Interfaces;
using System.Linq;
using Nocturne.BL.Helpers;

namespace Nocturne.BL.Services
{
    public class InMemorySessionService : ISessionService
    {
        private readonly List<SessionDto> _sessions = new List<SessionDto>
        {
                new SessionDto
                {
                    Id = 1,
                    CardId = 1,
                    ClientId = 1,
                    From = new DateTime(2015,06,13),
                    RegisteredBy = 1
                },
                new SessionDto
                {
                    Id = 2,
                    CardId = 1,
                    ClientId = 1,
                    From = new DateTime(2015,06,13, 15,00,00),
                    To =  new DateTime(2015,06,13, 16,31,40) ,
                    RegisteredBy = 1
                }
        };

        private readonly List<UsedProductDto> _usedProducts = new List<UsedProductDto>
        {
                new UsedProductDto
                {
                    Id = 1,
                    Amount = 2,
                    Date =  new DateTime(2015,06,13, 10,10,00) ,
                    ProductId = 1,
                    SessionId = 1,
                    RegisteredBy = 1
                },
                new UsedProductDto
                {
                    Id = 2,
                    Amount = 1,
                    Date =  new DateTime(2015,06,13, 15,10,00) ,
                    ProductId = 1,
                    SessionId = 2,
                    RegisteredBy = 1
                }
        };

        public SessionDto[] Find(Func<SessionDto, bool> predicate)
        {
            return _sessions.Where(predicate).ToArray();
        }

        public SessionDto GetSession(int id)
        {
            return _sessions.Where(q => q.Id == id).SingleOrDefault();
        }

        public ValidationResult<int> StartSession(SessionDto sessionDto)
        {
            var validationResult = ValidateSession(sessionDto);
            validationResult.ValidateProperty((msg) => { return sessionDto.Id != 0 ? new ValidationErrorMessage(msg) : null; }, "Can't start exist session");

            var existSesion = Find(s => s.CardId == sessionDto.CardId && !s.To.HasValue).SingleOrDefault();
            validationResult.ValidateProperty((msg) => { return existSesion != null ? new ValidationErrorMessage(msg) : null; }, $"Session already started at {existSesion?.From: dd.MM.yyyy HH:mm}", nameof(sessionDto.CardId));  
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }

            _sessions.Add(sessionDto);
            sessionDto.Id = _sessions.Count;
            validationResult.Result = sessionDto.Id;
            return validationResult;
        }

        public ValidationResult<int> StopSession(SessionDto sessionDto)
        {
            var validationResult = ValidateSession(sessionDto);
            validationResult.ValidateProperty((msg) => { return sessionDto.Id == 0 ? new ValidationErrorMessage(msg) : null; }, "Can't stop a session that doesn't exist");
            //validationResult.ValidateProperty((msg) => { return sessionDto.Id != 0 ? new ValidationErrorMessage(msg) : null; }, "Can't start exist session");
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }

            _sessions.RemoveAll(u => u.Id == sessionDto.Id);
            _sessions.Add(sessionDto);
            validationResult.Result = sessionDto.Id;

            return validationResult;
        }

        public UsedProductDto[] Find(Func<UsedProductDto, bool> predicate)
        {
            return _usedProducts.Where(predicate).ToArray();
        } 

        public UsedProductDto GetUsedProduct(int usedProductId)
        {
            return _usedProducts.Where(q => q.Id == usedProductId).SingleOrDefault();
        }

        public ValidationResult<int> SaveUsedProduct(UsedProductDto usedProductDto)
        {
            var validationResult = ValidateUsedProduct(usedProductDto);
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }

            if (usedProductDto.Id > 0)
            {
                _usedProducts.RemoveAll(u => u.Id == usedProductDto.Id);
                _usedProducts.Add(usedProductDto);
                validationResult.Result = usedProductDto.Id;
            }
            else
            {
                _usedProducts.Add(usedProductDto);
                usedProductDto.Id = _usedProducts.Count;
                validationResult.Result = usedProductDto.Id;
            }
            return validationResult;
        }

        private ValidationResult<int> ValidateUsedProduct(UsedProductDto usedProductDto)
        {
            const string emptyErrorTemplate = "{0} can not be blank.";
            const string atLeastOneErrorTemplate = "{0} must be >= 1.";
            var result = new ValidationResult<int>();

            result.ValidateProperty((msg) => { return usedProductDto.ProductId <= 0 ? new ValidationErrorMessage(msg) : null; },
                string.Format(emptyErrorTemplate, "Product"),
                nameof(usedProductDto.ProductId));

            result.ValidateProperty((msg) => { return usedProductDto.Amount < decimal.One ? new ValidationErrorMessage(msg) : null; },
               string.Format(atLeastOneErrorTemplate, "Amount"),
               nameof(usedProductDto.Amount));

            result.ValidateProperty((msg) => { return usedProductDto.SessionId <= 0 ? new ValidationErrorMessage(msg) : null; },
               string.Format(emptyErrorTemplate, "Session"),
               nameof(usedProductDto.SessionId));

            return result;
        }

        private ValidationResult<int> ValidateSession(SessionDto sessionDto)
        {
            const string emptyErrorTemplate = "{0} can not be blank.";
            var result = new ValidationResult<int>();

            result.ValidateProperty((msg) => { return sessionDto.CardId <= 0 ? new ValidationErrorMessage(msg) : null; },
                string.Format(emptyErrorTemplate, "Card"),
                nameof(sessionDto.CardId));

            return result;
        }
    }
}
