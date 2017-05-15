using System;
using Nocturne.BL.DTO;
using System.Linq;
using Nocturne.BL.Helpers;
using Nocturne.BL.Interfaces;
using Nocturne.DAL;

namespace Nocturne.BL.Services
{
    public class ClientService : IClientService
    {
        public ClientDto[] Find(Func<ClientDto, bool> predicate)
        {
            using (var dc = new NocturneContext())
            {
                return SelectClient(dc.Clients).Where(predicate).ToArray();
            }
        }

        public ClientDto GetClient(int id)
        {
            using (var dc = new NocturneContext())
            {
                return SelectClient(dc.Clients).Where(q => q.Id == id).SingleOrDefault();
            }
        } 

        public ValidationResult<int> SaveClient(ClientDto clientDto)
        {
            var validationResult = ValidateClient(clientDto);

            var existClient = Find(s => s.IdCode == clientDto.IdCode && s.Id != clientDto.Id).SingleOrDefault();
            validationResult.ValidateProperty((msg) => { return existClient != null ? new ValidationErrorMessage(msg) : null; }, "Client with this id code already registered", nameof(clientDto.IdCode));

            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }
            using (var dc = new NocturneContext())
            {
                Client clientDb;
                if (clientDto.Id > 0)
                {
                    clientDb = dc.Clients.Single(q => q.Id == clientDto.Id);
                }
                else
                {
                    clientDb = new Client();
                    dc.Clients.Add(clientDb);
                }

                clientDb.Name = clientDto.Name;
                clientDb.IdCode = clientDto.IdCode;
                clientDb.Surname = clientDto.Surname;

                dc.SaveChanges();
                clientDto.Id = clientDb.Id;
                validationResult.Result = clientDto.Id;
            }
            return validationResult;
        }

      
        private ValidationResult<int> ValidateClient(ClientDto client)
        {
            const string emptyErrorTemplate = "{0} can not be blank.";
            var result = new ValidationResult<int>();

            result.ValidateProperty((msg) => { return string.IsNullOrEmpty(client.IdCode) ? new ValidationErrorMessage(msg) : null; },
                string.Format(emptyErrorTemplate, "IdCode"),
                nameof(client.IdCode));

            result.ValidateProperty((msg) => { return string.IsNullOrEmpty(client.Name) ? new ValidationErrorMessage(msg) : null; },
               string.Format(emptyErrorTemplate, "Name"),
               nameof(client.Name));

            result.ValidateProperty((msg) => { return string.IsNullOrEmpty(client.Surname) ? new ValidationErrorMessage(msg) : null; },
               string.Format(emptyErrorTemplate, "Surname"),
               nameof(client.Surname));

            return result;
        }

        private IQueryable<ClientDto> SelectClient(IQueryable<Client> query)
        {
            return query.Select(u => new ClientDto
            {
                Id = u.Id,
                Name = u.Name,
                IdCode = u.IdCode,
                Surname = u.Surname
            });
        }
    }

}

