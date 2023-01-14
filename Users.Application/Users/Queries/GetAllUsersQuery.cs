using AutoMapper;
using MediatR;
using Users.Application.Common.Interfaces;

namespace Users.Application.Users.Queries;
public record GetAllUsersQuery: IRequest<int>{}

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, int>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IDataAccess dataAccess, IMapper mapper)
    {
        _dataAccess = dataAccess;
        _mapper = mapper;
    }
    public async Task<int> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        await _dataAccess.GetAllUsers();
        return 1;
    }
} 
