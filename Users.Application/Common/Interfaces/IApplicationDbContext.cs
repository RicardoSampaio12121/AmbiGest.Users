using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Entities;

namespace Users.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Credentials> Credentials { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

