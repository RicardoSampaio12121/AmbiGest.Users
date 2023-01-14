using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Common;
using Users.Domain.Entities;

namespace Users.Domain.Events.UpdateUser;
public class UpdateUserCreatedEvent: BaseEvent
{
    public UpdateUserCreatedEvent(User user)
    {
        User = user;
    }
    public User User { get; set; }

}

