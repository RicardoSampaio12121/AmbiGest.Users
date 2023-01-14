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

namespace Users.Application.Credentials.Commands;
public record AddUserCommand: IRequest<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
}

public class AddUserCommandHandler: IRequestHandler<AddUserCommand, int>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMapper _mapper;

    public AddUserCommandHandler(IDataAccess dataAccess, IMapper mapper)
    {
        _dataAccess = dataAccess;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        //Todo: check if data is legit
        var entity = _mapper.Map<User>(request);
        entity.Role = "User";

        entity.AddDomainEvent(new AddCredentialsCreatedEvent(entity));
        var result = await _dataAccess.AddUser(entity);

        return 1;
    }
}

