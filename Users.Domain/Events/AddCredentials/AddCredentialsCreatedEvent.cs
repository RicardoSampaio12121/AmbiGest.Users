using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Common;
using Users.Domain.Entities;

namespace Users.Domain.Events.AddCredentials;
public class AddCredentialsCreatedEvent: BaseEvent
{
    public AddCredentialsCreatedEvent(Credentials credentials)
    {
        Credentials = credentials;
    }

    public Credentials Credentials { get; set; }
}
