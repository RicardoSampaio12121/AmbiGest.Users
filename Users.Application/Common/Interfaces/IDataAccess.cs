using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Entities;

namespace Users.Application.Common.Interfaces;
public interface IDataAccess
{
    Task AddUser(User user);
    Task UpdateUser(User user);
}
