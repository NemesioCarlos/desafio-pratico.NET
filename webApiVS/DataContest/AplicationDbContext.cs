using Microsoft.EntityFrameworkCore;
using webApiVS.Models;

namespace webApiVS.DataContest
{
    public class AplicationDbContext: DbContext 
    {       
        //padrão para fazer uma conexão com o banco usando o Entity
        public AplicationDbContext(DbContextOptions<AplicationDbContext>options) : base(options)
        {  
        }

        //Ao rodar essa linha o programa entende que tem que criar uma tabaela Contato com a estrutura: contatoMl
        public DbSet<ContatoModel> Contatos { get; set; } 

    }
}
