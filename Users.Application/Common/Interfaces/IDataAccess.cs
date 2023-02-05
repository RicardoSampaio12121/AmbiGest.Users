using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Entities;

namespace Users.Application.Common.Interfaces;
public interface IDataAccess
{
    Task<User> AddUser(User user);
    Task<User> UpdateUser(User user);
    Task UpdateEmail(string currentEmail, string newEmail);
    Task<User> DeleteUser(string email);
    Task<List<User>> GetUser(string email);
    Task<List<User>> GetAllUsers();
}
