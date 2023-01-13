using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.Common.Interfaces;
using Users.Domain.Entities;
using Users.Infrastructure.Common;

namespace Users.Infrastructure.Persistence;

public class ApplicationDbContext :DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;

    public ApplicationDbContext(IMediator mediator, 
        DbContextOptions<ApplicationDbContext> options)
        :base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Credentials> Credentials => Set<Credentials>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Credentials>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);
    }
}
