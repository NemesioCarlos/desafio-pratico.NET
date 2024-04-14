using Microsoft.EntityFrameworkCore;
using webApiVS.Models;

namespace webApiVS.DataContest
{
    public class AplicationDbContext: DbContext 
    {       

        public AplicationDbContext(DbContextOptions<AplicationDbContext>options) : base(options)
        {  
        }
        public DbSet<ContatoModel> Contatos { get; set; } 

    }
}
