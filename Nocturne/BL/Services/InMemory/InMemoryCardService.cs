using System;
using System.Collections.Generic;
using Nocturne.BL.DTO;
using Nocturne.BL.Interfaces;
using System.Linq;
using Nocturne.BL.Helpers;

namespace Nocturne.BL.Services
{
    public class InMemoryCardService : ICardService
    {
        private readonly Dictionary<int, int> _userCards = new Dictionary<int, int>
        {
            { 3, 4}
        };

        private readonly Dictionary<int, int> _clientCards = new Dictionary<int, int>
        {
            { 2, 3}
        };

        private readonly List<CardDto> _cards = new List<CardDto>
        {
                new CardDto
                {
                    Id = 1,
                   CardType = CardTypeEnum.IdCard,
                   RegCard = "47549732407;Mari-Liis;Kask"
                },
                new CardDto
                {
                    Id = 2,
                   CardType = CardTypeEnum.RfidCard,
                   Uid = 1020304050607
                },
                new CardDto
                {
                    Id = 3,
                   CardType = CardTypeEnum.RfidCard,
                   Uid = 1894916139
                }
        };

        public int? GetUserIdAssotiatedWithCard(int cardId)
        {
            if (_userCards.ContainsKey(cardId))
            {
                return _userCards[cardId];
            }
            return null;
        }

        public int? GetClientIdAssotiatedWithCard(int cardId)
        {
            if (_clientCards.ContainsKey(cardId))
            {
                return _clientCards[cardId];
            }
            return null;
        }

        public ValidationResult<bool> AssotiateCardWithUser(int userId, int cardId)
        {
            var validationResult = new ValidationResult<bool>();

            validationResult.ValidateProperty((msg) => { return _userCards.ContainsKey(cardId) ? new ValidationErrorMessage(msg) : null; },
               "Card assotiated with other user",
               "CardId");
            if (!validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }
            _userCards.Add(cardId, userId);
            validationResult.Result = true;
            return validationResult;
        }

        public ValidationResult<bool> AssotiateCardWithClient(int clientId, int cardId)
        {
            var validationResult = new ValidationResult<bool>();

            validationResult.ValidateProperty((msg) => { return _clientCards.ContainsKey(cardId) ? new ValidationErrorMessage(msg) : null; },
               "Card assotiated with another client",
               "CardId");
            if (!validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }
            _clientCards.Add(cardId, clientId);
            validationResult.Result = true;
            return validationResult;
        }

        public CardDto[] Find(Func<CardDto, bool> predicate)
        {
            return _cards.Where(predicate).ToArray();
        }

        public CardDto GetCard(int id)
        {

            return _cards.Where(q => q.Id == id).SingleOrDefault();
        }

        public ValidationResult<int> SaveCard(CardDto cardDto)
        {
            var validationResult = ValidateCard(cardDto);
            validationResult.ValidateProperty((msg) => { return cardDto.Id != 0 ? new ValidationErrorMessage(msg) : null; }, "Can't change exist card");

            var existCard = Find(s => s.DisplayName == cardDto.DisplayName && s.CardType == cardDto.CardType).SingleOrDefault();
            validationResult.ValidateProperty((msg) => { return existCard != null ? new ValidationErrorMessage(msg) : null; }, $"Exist card with same UID");
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }

            _cards.Add(cardDto);
            cardDto.Id = _cards.Count;
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
    }
}
