using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Application.Mappers
{
    public static class ClientMap
    {
        public static Client ToClient(this ClientRequest request) => new Client
        {
            Name = request.Name,
            Password = request.Password,
            Status = request.Status != null ? request.Status.Value : Core.Enums.StatusEnum.Active,
        };

        public static ClientResponse ToClientResponse(this Client client) => new ClientResponse
        {
            Id = client.Id,
            Name = client.Name,
            Status = client.Status,
            Profiles = client.ClientProfiles?.Where(w => w.Profile != null).Select(s => s.Profile.ToProfileResponse()).ToList()
        };

        public static void Update(this Client client, ClientUpdateRequest request)
        {
            client.Name = request.Name;
            client.Status = request.Status != null ? request.Status.Value : client.Status;
        }
    }
}