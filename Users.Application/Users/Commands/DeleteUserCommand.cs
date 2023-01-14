using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Application.Common.Interfaces;

namespace Users.Application.Users.Commands;
public record DeleteUserCommand: IRequest<int> 
{
    public string Email { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, int>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMapper _mapper;

    public DeleteUserCommandHandler(IDataAccess dataAccess, IMapper mapper)
    {
        _dataAccess = dataAccess;
        _mapper = mapper;
    }

    public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        //TODO: Check if data is legit
        await _dataAccess.DeleteUser(request.Email);
        return 1;
    }
}
