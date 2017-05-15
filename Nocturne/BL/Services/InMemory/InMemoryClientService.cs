using System;
using System.Collections.Generic;
using Nocturne.BL.DTO;
using System.Linq;
using Nocturne.BL.Helpers;
using Nocturne.BL.Interfaces;

namespace Nocturne.BL.Services
{
    public class InMemoryClientService : IClientService
    {
        private readonly List<ClientDto> _clients = new List<ClientDto>
        {
                new ClientDto
                {
                    Id = 1,
                    Name = "Mike",
                    Surname = "Cartwright",
                    IdCode = "37529732275",
                },
                 new ClientDto
                {
                    Id = 2,
                    Name = "Gandalf",
                    Surname = "White",
                    IdCode = "420420420",   
                },
                  new ClientDto
                {
                    Id = 3,
                    Name = "Gandalf",
                    Surname = "Grey",
                    IdCode = "42354634634",
                }
        };

        public ClientDto[] Find(Func<ClientDto, bool> predicate)
        {
            return _clients.Where(predicate).ToArray();
        }

        public ClientDto GetClient(int id)
        {
            return _clients.Where(q => q.Id == id).SingleOrDefault();
        }

        

        public ValidationResult<int> SaveClient(ClientDto client)
        {
            var validationResult = ValidateClient(client);
            if (validationResult.HasValidationMessageType<ValidationErrorMessage>()) { return validationResult; }
            if (client.Id > 0)
            {
                _clients.RemoveAll(u => u.Id == client.Id);
                _clients.Add(client);
                validationResult.Result = client.Id;
            }
            else
            {
                _clients.Add(client);
                client.Id = _clients.Count;
                validationResult.Result = client.Id;
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
    }

}

