using System;

namespace Family.Accounts.Management.Infrastructure.Responses
{
    public class ProfileResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}