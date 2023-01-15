using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Application.Common.Interfaces;
using Users.Domain.Entities;
using Users.Domain.Enums;
using Users.Domain.Events.AddCredentials;

namespace Users.Application.Users.Commands.AddUserCommand;
public record AddUserCommand : IRequest<User>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
}

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMapper _mapper;

    public AddUserCommandHandler(IDataAccess dataAccess, IMapper mapper)
    {
        _dataAccess = dataAccess;
        _mapper = mapper;
    }

    public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        //Todo: check if data is legit
        var entity = _mapper.Map<User>(request);
        try
        {
            var result = await _dataAccess.AddUser(entity);
            return result;
        }
        catch
        {
            //TODO: Use the log service here
            throw;
        }
    }
}

