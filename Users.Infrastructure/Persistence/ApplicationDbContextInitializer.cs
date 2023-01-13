using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Users.Infrastructure.Persistence;
public class ApplicationDbContextInitializer
{

    private readonly ApplicationDbContext _context;

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }catch(Exception ex)
        {
            throw;
        }
    }
}
