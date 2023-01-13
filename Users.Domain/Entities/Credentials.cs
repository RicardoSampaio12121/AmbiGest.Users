﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Common;

namespace Users.Domain.Entities;
public class Credentials: BaseEntity
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Code { get; set; }
}
