using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Users.Application.Common.Interfaces;
using Users.Domain.Entities;
using Users.Domain.Events.AddCredentials;

namespace Users.Application.Credentials.Commands;
public record AddCredentialsCommand: IRequest<int>
{
    public string Name { get; set; }
}

public class AddCredentialsCommandHandler: IRequestHandler<AddCredentialsCommand, int>
{
    private readonly IApplicationDbContext _context;

    public AddCredentialsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddCredentialsCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Entrou no handler");

        var entity = new Domain.Entities.Credentials()
        {
            Email = "teste@teste.com",
            Password = "password",
            Role = "role",
            Code = "code"
        };

        entity.AddDomainEvent(new AddCredentialsCreatedEvent(entity));
        _context.Credentials.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

