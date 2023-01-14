using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Application.Common.Interfaces;
using Users.Domain.Entities;
using Users.Domain.Events.UpdateUser;

namespace Users.Application.Users.Commands;
public record UpdateUserCommand: IRequest<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
}

public class UpdateUserCommandHandler: IRequestHandler<UpdateUserCommand, int>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IDataAccess dataAccess, IMapper mapper)
    {
        _dataAccess = dataAccess;
        _mapper = mapper;
    }

    public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        //TODO: Check if data is legit
        var toUpdate = _mapper.Map<User>(request);
        toUpdate.AddDomainEvent(new UpdateUserCreatedEvent(toUpdate));

        await _dataAccess.UpdateUser(toUpdate);

        //TODO: Find a way to return a string so that I can return the ID
        return 1;

    }
}
