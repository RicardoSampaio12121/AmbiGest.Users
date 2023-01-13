using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Credentials.Commands;
public record AddCredentialsCommand: IRequest<int>
{
    public string Name { get; set; }
}

public class AddCredentialsCommandHandler : IRequestHandler<AddCredentialsCommand, int>
{
    private readonly IApplicationDbContext _context;

    public AddCredentialsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddCredentialsCommand request, CancellationToken cancellationToken)
    {
        var entity = new CleanArchitecture.Domain.Entities.Credentials()
        {
            Email = "teste@teste.com",
            Password= "password",
            Role = "role",
            Code = "code"
        };

        entity.AddDomainEvent(new Domain.Events.AddCredentials.AddCredentialsCreatedEvent(entity));
        _context.Credentials.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
