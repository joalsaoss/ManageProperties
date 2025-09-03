using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageProperties.Domain.Exceptions
{
    public class BusinessRulesExceptions:Exception
    {
        public BusinessRulesExceptions(string message) : base(message)
        {
        }
    }
}
