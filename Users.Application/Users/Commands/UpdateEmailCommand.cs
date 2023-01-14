using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Application.Common.Interfaces;

namespace Users.Application.Users.Commands;
public record UpdateEmailCommand: IRequest<int>
{
    public string CurrentEmail { get; set; }
    public string NewEmail { get; set; }
}

public class UpdateEmailCommandHandler : IRequestHandler<UpdateEmailCommand, int>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMapper _mapper;

    public UpdateEmailCommandHandler(IDataAccess dataAccess, IMapper mapper)
    {
        _dataAccess = dataAccess;
        _mapper = mapper;
    }

    public async Task<int> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
    {
        //TODO: Check if data is legit
        var result = _dataAccess.UpdateEmail(request.CurrentEmail, request.NewEmail);
        return 1;
    }
}
