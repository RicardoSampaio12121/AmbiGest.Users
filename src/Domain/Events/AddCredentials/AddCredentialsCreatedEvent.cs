using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Events.AddCredentials;
public class AddCredentialsCreatedEvent: BaseEvent
{
    public AddCredentialsCreatedEvent(Credentials credentials)
    {
        Credentials = credentials;
    }

    public Credentials Credentials { get; set; }
}
