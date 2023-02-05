using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Application.Common.Interfaces;
using Users.Domain.Dtos;
using Users.Domain.Entities;
using Users.Domain.Enums;
using Users.Domain.Exceptions;
using Users.Domain.Interfaces;

namespace Users.Application.Users.Queries;
public record GetUserQuery: IRequest<User>
{
    public string Email{ get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMapper _mapper;
    private readonly IRabbitMQClient _rabbitMQClient;

    public GetUserQueryHandler(IDataAccess dataAccess, IMapper mapper, IRabbitMQClient rabbitMQClient)
    {
        _dataAccess = dataAccess;
        _mapper = mapper;
        _rabbitMQClient = rabbitMQClient;
    }

    public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var loggingDto = new GenericLoggingDto(LogEventTypes.Add_Log.ToString());
        var expectedException = "";
        try
        {
            var result = await _dataAccess.GetUser(request.Email);
            if(result == null)
            {
                loggingDto.LogType = LogTypes.error.ToString();
                loggingDto.Message = $"User {request.Email} has tried to get his information but it does not exists.";
                expectedException = "WrongEmailException";
                throw new WrongEmailException();
            }

            var user = result[0];

            loggingDto.LogType = LogTypes.information.ToString();
            loggingDto.Message = $"User {request.Email} has retrieved his information.";
            return user;
            
        }catch(Exception ex)
        {
            if(expectedException != "WrongEmailException")
            {
                loggingDto.LogType = LogTypes.error.ToString();
                loggingDto.Message = $"User {request.Email} has tried to get his information but got the following error: {ex.Message}";
            }
            throw;
        }
        finally
        {
            _rabbitMQClient.SendMessage(Newtonsoft.Json.JsonConvert.SerializeObject(loggingDto));
        }
    }
}
