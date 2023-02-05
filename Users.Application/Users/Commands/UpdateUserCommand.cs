using AutoMapper;
using MediatR;
using Users.Application.Common.Interfaces;
using Users.Domain.Dtos;
using Users.Domain.Entities;
using Users.Domain.Enums;
using Users.Domain.Exceptions;
using Users.Domain.Interfaces;

namespace Users.Application.Users.Commands;
public record UpdateUserCommand: IRequest<User>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
}

public class UpdateUserCommandHandler: IRequestHandler<UpdateUserCommand, User>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMapper _mapper;
    private readonly IRabbitMQClient _rabbitMQClient;

    public UpdateUserCommandHandler(IDataAccess dataAccess, IMapper mapper, IRabbitMQClient rabbitMQClient)
    {
        _dataAccess = dataAccess;
        _mapper = mapper;
        _rabbitMQClient = rabbitMQClient;
    }

    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var loggingDto = new GenericLoggingDto(LogEventTypes.Add_Log.ToString());
        var expectedException = "";
        try
        {
            var toUpdate = _mapper.Map<User>(request);
            var updated = await _dataAccess.UpdateUser(toUpdate);

            if (updated == null)
            {
                loggingDto.LogType = LogTypes.error.ToString();
                loggingDto.Message = $"User {request.Email} tried to update his information but it does not exists.";
                expectedException = "WrongEmailException";
                throw new WrongEmailException();
            }

            loggingDto.LogType = LogTypes.information.ToString();
            loggingDto.Message = $"User {request.Email} has updated his information.";

            return updated;
        }
        catch(Exception ex)
        {
            if(expectedException != "ErongEmailException")
            {
                loggingDto.LogType = LogTypes.error.ToString();
                loggingDto.Message = $"User {request.Email} has tried to update his information but got the following error: {ex.Message}.";
            }
            throw;
        }
        finally
        {
            _rabbitMQClient.SendMessage(Newtonsoft.Json.JsonConvert.SerializeObject(loggingDto));
        }
    }
}
