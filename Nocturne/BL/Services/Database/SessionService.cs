using System;
using System.Collections.Generic;
using Nocturne.BL.DTO;
using Nocturne.BL.Interfaces;
using System.Linq;
using Nocturne.BL.Helpers;
using Nocturne.DAL;

namespace Nocturne.BL.Services
{
    public class SessionService : ISessionService
    {
        public SessionDto[] Find(Func<SessionDto, bool> predicate)
        {
            using (var dc = new NocturneContext())
            {
                return SelectSession(dc.Sessions).Where(predicate).ToArray();
            }
        }

        public SessionDto GetSession(int id)
        {
            using (var dc = new NocturneContext())
            {
                return SelectSession(dc.Sessions).Where(q => q.Id == id).SingleOrDefault();
            }
        }

        public ValidationResult<int> StartSession(SessionDto sessionDto)
        {
            var validationResult = ValidateSession(sessionDto);
            validationResult.ValidateProperty((msg) => { return sessionDto.Id != 0 ? new ValidationErrorMessage(msg) : null; }, "Can't start exist session");

            var existSesion = Find(s => s.CardId == sessionDto.CardId && !s.To.HasValue).SingleOrDefault();
            validationResult.ValidateProperty((msg) => { return existSesion != null ? new ValidationErrorMessage(msg) : null; }, $"Session already started at {existSesion?.From: dd.MM.yyyy HH:mm}", nameof(sessionDto.CardId));
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }

            using (var dc = new NocturneContext())
            {
                var sessionDb = new Session
                {
                    CardId = sessionDto.CardId,
                    ClientId = sessionDto.ClientId,
                    From = sessionDto.From,
                    RegisteredById = sessionDto.RegisteredBy
                };
                dc.Sessions.Add(sessionDb);
                dc.SaveChanges();
                sessionDto.Id = sessionDb.Id;
                validationResult.Result = sessionDb.Id;
            }
            return validationResult;
        }

        public ValidationResult<int> StopSession(SessionDto sessionDto)
        {
            var validationResult = ValidateSession(sessionDto);
            validationResult.ValidateProperty((msg) => { return sessionDto.Id == 0 ? new ValidationErrorMessage(msg) : null; }, "Can't stop a session that doesn't exist");
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }

            using (var dc = new NocturneContext())
            {
                var session = dc.Sessions.Where(q => q.Id == sessionDto.Id).Single();
                session.To = sessionDto.To;
                dc.SaveChanges();
            }
            validationResult.Result = sessionDto.Id;
            return validationResult;
        }

        public UsedProductDto[] Find(Func<UsedProductDto, bool> predicate)
        {
            using (var dc = new NocturneContext())
            {
                return SelectUsedProduct(dc.UsedProducts).Where(predicate).ToArray();
            }
        }

        public UsedProductDto GetUsedProduct(int usedProductId)
        {
            using (var dc = new NocturneContext())
            {
                return SelectUsedProduct(dc.UsedProducts).Where(q => q.Id == usedProductId).SingleOrDefault();
            }
        }

        public ValidationResult<int> SaveUsedProduct(UsedProductDto usedProductDto)
        {
            var validationResult = ValidateUsedProduct(usedProductDto);
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }

            using (var dc = new NocturneContext())
            {
                UsedProduct usedProductDb;
                if (usedProductDto.Id > 0)
                {
                    usedProductDb = dc.UsedProducts.Single(q => q.Id == usedProductDto.Id);
                }
                else
                {
                    usedProductDb = new UsedProduct();
                    dc.UsedProducts.Add(usedProductDb);
                }

                usedProductDb.Amount = usedProductDto.Amount;
                usedProductDb.Date = usedProductDto.Date;
                usedProductDb.ProductId = usedProductDto.ProductId;
                usedProductDb.RegisteredById = usedProductDto.RegisteredBy;
                usedProductDb.SessionId = usedProductDto.SessionId;

                dc.SaveChanges();
                usedProductDto.Id = usedProductDb.Id;
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

        private IQueryable<SessionDto> SelectSession(IQueryable<Session> query)
        {
            return query.Select(u => new SessionDto
            {
                To = u.To,
                RegisteredBy = u.RegisteredById,
                Id = u.Id,
                CardId = u.CardId,
                ClientId = u.ClientId,
                From = u.From
            });
        }

        private IQueryable<UsedProductDto> SelectUsedProduct(IQueryable<UsedProduct> query)
        {
            return query.Select(u => new UsedProductDto
            {
                Amount = u.Amount,
                Date = u.Date,
                Id = u.Id,
                ProductId = u.ProductId,
                RegisteredBy = u.RegisteredById,
                SessionId = u.SessionId
            });
        }
    }
}
