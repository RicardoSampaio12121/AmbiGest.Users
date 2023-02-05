using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Domain.Dtos;
public class GenericLoggingDto
{
    public string ServiceName { get; set; } = "UsersService";
    public string LogType { get; set; }
    public string Message { get; set; }
    public string Event { get; set; }

    public GenericLoggingDto(string Event)
    {
        this.Event = Event;
    }

    public GenericLoggingDto(string LogType, string Message, string Event)
    {
        this.LogType = LogType;
        this.Message = Message;
        this.Event = Event;
    }
}
