using AutoMapper;
using MediatR;
using Users.Application.Common.Interfaces;
using Users.Domain.Dtos;
using Users.Domain.Entities;
using Users.Domain.Enums;
using Users.Domain.Interfaces;

namespace Users.Application.Users.Queries;
public record GetAllUsersQuery: IRequest<List<User>>{ }

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMapper _mapper;
    private readonly IRabbitMQClient _rabbitMQClient;

    public GetAllUsersQueryHandler(IDataAccess dataAccess, IMapper mapper, IRabbitMQClient rabbitMQClient)
    {
        _dataAccess = dataAccess;
        _mapper = mapper;
        _rabbitMQClient = rabbitMQClient;
    }
    public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var loggingDto = new GenericLoggingDto(LogEventTypes.Add_Log.ToString());

        try
        {
            var users = await _dataAccess.GetAllUsers();
            loggingDto.LogType = LogTypes.information.ToString();
            loggingDto.Message = "All user have been returned";
            return users;
        }
        catch (Exception ex)
        {
            loggingDto.LogType = LogTypes.error.ToString();
            loggingDto.Message = "There was an error trying to return all users.";
            throw;
        }
        finally
        {
            _rabbitMQClient.SendMessage(Newtonsoft.Json.JsonConvert.SerializeObject(loggingDto));
        }
    }
} 
