using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHB.Domain.Exceptions
{
    public class InvalidOperationException : Exception
    {
        public InvalidOperationException(string message) : base(message)
        {
        }

        public InvalidOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
