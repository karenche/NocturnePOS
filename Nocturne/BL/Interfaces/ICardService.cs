using System;
using Nocturne.BL.DTO;
using Nocturne.BL.Helpers;

namespace Nocturne.BL.Interfaces
{
    /// <summary>
    /// Service to manage card and user/client relationships.
    /// </summary>
    public interface ICardService
    {
        CardDto[] Find(Func<CardDto, bool> predicate);
        CardDto GetCard(int id);
        ValidationResult<int> SaveCard(CardDto cardDto);

        ValidationResult<bool> AssotiateCardWithUser(int userId, int cardId);
        int? GetUserIdAssotiatedWithCard(int cardId);

        ValidationResult<bool> AssotiateCardWithClient(int customerId, int cardId);
        int? GetClientIdAssotiatedWithCard(int cardId);
    }
}