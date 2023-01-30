using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
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

