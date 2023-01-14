using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Application.Common.Interfaces;

namespace Users.Application.Users.Queries;
public record GetUserQuery: IRequest<int>
{
    public string Email{ get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, int>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IDataAccess dataAccess, IMapper mapper)
    {
        _dataAccess = dataAccess;
        _mapper = mapper;
    }

    public async Task<int> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var result = _dataAccess.GetUser(request.Email);
        return 1;
    }
}
