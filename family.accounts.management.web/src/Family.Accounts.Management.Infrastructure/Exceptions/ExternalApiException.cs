using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family.Accounts.Management.Infrastructure.Exceptions
{
    public class ExternalApiException : Exception
    {
        public ExternalApiException(string message) : base(message)
        {
        }
    }
}