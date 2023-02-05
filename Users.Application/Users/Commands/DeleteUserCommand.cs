using AutoMapper;
using MediatR;
using Users.Application.Common.Interfaces;
using Users.Domain.Constants;
using Users.Domain.Dtos;
using Users.Domain.Enums;
using Users.Domain.Exceptions;
using Users.Domain.Interfaces;

namespace Users.Application.Users.Commands;
public record DeleteUserCommand: IRequest<Unit> 
{
    public string Email { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IDataAccess _dataAccess;
    private readonly IMapper _mapper;
    private readonly IRabbitMQClient _rabbitMQClient;

    public DeleteUserCommandHandler(IDataAccess dataAccess, IMapper mapper, IRabbitMQClient rabbitMQClient)
    {
        _dataAccess = dataAccess;
        _mapper = mapper;
        _rabbitMQClient = rabbitMQClient;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var loggingDto = new GenericLoggingDto(LogEventTypes.Add_Log.ToString());
        var expectedException = "";
        try
        {
            var res = await _dataAccess.DeleteUser(request.Email);
            if(res == null)
            {
                loggingDto.LogType = LogTypes.error.ToString();
                loggingDto.Message = LoggingMessages.DeleteUserSuccessfully(request.Email);
                expectedException = "WrongEmailException";
                throw new WrongEmailException();
            }

            loggingDto.LogType = LogTypes.information.ToString();
            loggingDto.Message = LoggingMessages.DeleteUserSuccessfully(request.Email);

            return new Unit();
        }
        catch(Exception ex)
        {
            if(expectedException != "WrongEmailException")
            {
                loggingDto.LogType = LogTypes.error.ToString();
                loggingDto.Message = LoggingMessages.DeleteUserError(request.Email, ex.Message);
            }
            throw;
        }
        finally
        {
            _rabbitMQClient.SendMessage(Newtonsoft.Json.JsonConvert.SerializeObject(loggingDto));
        }
        
    }
}
