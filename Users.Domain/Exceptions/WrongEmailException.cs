using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Domain.Exceptions;
public class WrongEmailException: Exception
{
    public WrongEmailException(): base("Wrong email exception")
    {

    }
}
