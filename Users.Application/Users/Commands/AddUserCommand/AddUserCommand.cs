using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Application.Common.Interfaces;
using Users.Domain.Constants;
using Users.Domain.Dtos;
using Users.Domain.Entities;
using Users.Domain.Enums;
using Users.Domain.Events.AddCredentials;
using Users.Domain.Exceptions;
using Users.Domain.Interfaces;

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
    private readonly IRabbitMQClient _rabbitMQClient;

    public AddUserCommandHandler(IDataAccess dataAccess, IMapper mapper, IRabbitMQClient rabbitMQClient)
    {
        _dataAccess = dataAccess;
        _mapper = mapper;
        _rabbitMQClient = rabbitMQClient;
    }

    public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var loggingDto = new GenericLoggingDto(LogEventTypes.Add_Log.ToString());
        var expectedException = "";
        try
        {
            var entity = _mapper.Map<User>(request);
            
            var existing = await _dataAccess.GetUser(request.Email);
            if(existing == null || existing.Count == 0)
            {
                loggingDto.LogType = LogTypes.error.ToString();
                loggingDto.Message = LoggingMessages.AddUserUserExists(request.Email);
                expectedException = "WrongEmailException";
                throw new WrongEmailException();
            }

            var result = await _dataAccess.AddUser(entity);
            loggingDto.LogType = LogTypes.information.ToString();
            loggingDto.Message = LoggingMessages.AddUserSuccessfully(request.Email);

            return result;
        }
        catch(Exception ex) 
        {
            if (expectedException != "WrongEmailException")
            {
                loggingDto.LogType = LogTypes.error.ToString();
                loggingDto.Message = LoggingMessages.AddUserError(request.Email, ex.Message);
            }
            throw;
        }
        finally
        {
            _rabbitMQClient.SendMessage(Newtonsoft.Json.JsonConvert.SerializeObject(loggingDto));
        }
    }
}

