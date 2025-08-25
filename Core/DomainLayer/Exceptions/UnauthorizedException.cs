using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class UnauthorizedException(string message = "Email or Password is Invalid") : Exception(message: message)
    {
    }
}
