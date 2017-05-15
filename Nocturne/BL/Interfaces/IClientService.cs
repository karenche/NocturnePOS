using System;
using Nocturne.BL.DTO;
using Nocturne.BL.Helpers;

namespace Nocturne.BL.Interfaces
{
    /// <summary>
    /// Service interface to manage active clients.
    /// </summary>
    public interface IClientService
    {
        ClientDto[] Find(Func<ClientDto, bool> predicate);
        ClientDto GetClient(int id);
        ValidationResult<int> SaveClient(ClientDto customerDto);
    }
}

