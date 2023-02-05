using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Domain.Constants;
public static class LoggingMessages
{
    public static string AddUserUserExists(string email)
    {
        return $"User is trying to add an entry with an already in use email: {email}.";
    }

    public static string AddUserSuccessfully(string email)
    {
        return $"User {email} created successfully.";
    }

    public static string AddUserError(string email, string error)
    {
        return $"User {email} has tried to create an entry with the following error: {error}";
    }

    public static string DeleteUserSuccessfully(string email)
    {
        return $"User {email} tried to delete it's entry but it does not exists.";
    }

    public static string DeleteUserError(string email, string error)
    {
        return $"User {email} tried to delete it's account but got the following error: {error}";
    }

    public static string DeleteUserSuccessfullt(string email)
    {
        return $"User {email} deleted it's account successfully.";
    }
}
