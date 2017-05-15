using System;
using Nocturne.BL.DTO;
using Nocturne.BL.Helpers;

namespace Nocturne.BL.Interfaces
{
    /// <summary>
    /// Service to manage active client sessions.
    /// </summary>
    public interface ISessionService
    {
        UsedProductDto[] Find(Func<UsedProductDto, bool> predicate);
        SessionDto[] Find(Func<SessionDto, bool> predicate);
        SessionDto GetSession(int id);
        UsedProductDto GetUsedProduct(int usedProductId);
        ValidationResult<int> StartSession(SessionDto sessionDto);
        ValidationResult<int> StopSession(SessionDto sessionDto);
        ValidationResult<int> SaveUsedProduct(UsedProductDto usedProductDto);
    }
}