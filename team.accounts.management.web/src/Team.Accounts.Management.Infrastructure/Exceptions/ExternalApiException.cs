using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team.Accounts.Management.Infrastructure.Exceptions
{
    public class ExternalApiException : Exception
    {
        public ExternalApiException(string message) : base(message)
        {
        }

        public ExternalApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}