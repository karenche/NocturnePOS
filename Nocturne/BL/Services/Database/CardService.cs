using System;
using Nocturne.BL.DTO;
using Nocturne.BL.Interfaces;
using System.Linq;
using Nocturne.BL.Helpers;
using Nocturne.DAL;

namespace Nocturne.BL.Services
{
    public class CardService : ICardService
    {
        public int? GetUserIdAssotiatedWithCard(int cardId)
        {
            using (var dc = new NocturneContext())
            {
                return dc.Cards.Where(q => q.Id == cardId).Select(q => q.UserId).Single();
            }
        }

        public int? GetClientIdAssotiatedWithCard(int cardId)
        {
            using (var dc = new NocturneContext())
            {
                return dc.Cards.Where(q => q.Id == cardId).Select(q => q.ClientId).Single();
            }
        }

        public ValidationResult<bool> AssotiateCardWithUser(int userId, int cardId)
        {
            var validationResult = new ValidationResult<bool>();
            using (var dc = new NocturneContext())
            {
                var card = dc.Cards.Where(q => q.Id == cardId).Single();
                validationResult.ValidateProperty((msg) => { return card.UserId.HasValue ? new ValidationErrorMessage(msg) : null; },
                   "Card assotiated with other user",
                   "CardId");
                if (!validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }
                card.UserId = userId;
                dc.SaveChanges();
            }
            validationResult.Result = true;
            return validationResult;
        }

        public ValidationResult<bool> AssotiateCardWithClient(int clientId, int cardId)
        {
            var validationResult = new ValidationResult<bool>();
            using (var dc = new NocturneContext())
            {
                var card = dc.Cards.Where(q => q.Id == cardId).Single();
                validationResult.ValidateProperty((msg) => { return card.ClientId.HasValue ? new ValidationErrorMessage(msg) : null; },
               "Card assotiated with another client",
               "CardId");
                if (!validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }
                card.ClientId = cardId;
                dc.SaveChanges();
            }
            validationResult.Result = true;
            return validationResult;
        }

        public CardDto[] Find(Func<CardDto, bool> predicate)
        {
            using (var dc = new NocturneContext())
            {
                return SelectCard(dc.Cards).Where(predicate).ToArray();
            }
        }

        public CardDto GetCard(int id)
        {
            using (var dc = new NocturneContext())
            {
                return SelectCard(dc.Cards).Where(q => q.Id == id).SingleOrDefault();
            }
        }

        public ValidationResult<int> SaveCard(CardDto cardDto)
        {
            var validationResult = ValidateCard(cardDto);
            validationResult.ValidateProperty((msg) => { return cardDto.Id != 0 ? new ValidationErrorMessage(msg) : null; }, "Can't change exist card");

            var existCard = Find(s => s.DisplayName == cardDto.DisplayName && s.CardType == cardDto.CardType).SingleOrDefault();
            validationResult.ValidateProperty((msg) => { return existCard != null ? new ValidationErrorMessage(msg) : null; }, $"Exist card with same UID");
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }

            using (var dc = new NocturneContext())
            {
                var cardDb = new Card
                {
                    Uid = cardDto.Uid,
                    CardType = (int)cardDto.CardType,
                    Firstname = cardDto.Firstname,
                    Lastname = cardDto.Lastname,
                    RegCard = cardDto.RegCard,
                };
                dc.Cards.Add(cardDb);
                dc.SaveChanges();
                cardDto.Id = cardDb.Id;
            }
            validationResult.Result = cardDto.Id;
            return validationResult;
        }

        private ValidationResult<int> ValidateCard(CardDto cardDto)
        {
            const string emptyErrorTemplate = "{0} can not be blank.";
            var result = new ValidationResult<int>();

            result.ValidateProperty((msg) => { return string.IsNullOrEmpty(cardDto.DisplayName) || cardDto.DisplayName == "0" ? new ValidationErrorMessage(msg) : null; },
                string.Format(emptyErrorTemplate, "Display name"),
                nameof(cardDto.DisplayName));

            return result;
        }

        private IQueryable<CardDto> SelectCard(IQueryable<Card> query)
        {
            return query.Select(u => new CardDto
            {
                Id = u.Id,
                CardType = (CardTypeEnum)u.CardType,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                RegCard = u.RegCard,
                Uid = u.Uid
            });
        }
    }
}
