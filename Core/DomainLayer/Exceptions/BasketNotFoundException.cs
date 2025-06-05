using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class BasketNotFoundException(string id) : NotFoundException(message: $"Basket with Id = {id} is not found")
    {
    }
}
