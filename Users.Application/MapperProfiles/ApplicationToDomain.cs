using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Users.Application.Users.Commands;
using Users.Application.Users.Commands.AddUserCommand;
using Users.Domain.Entities;

namespace Users.Application.MapperProfiles;
public class ApplicationToDomain: Profile
{
    public ApplicationToDomain()
    {
        CreateMap<AddUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
    }
}