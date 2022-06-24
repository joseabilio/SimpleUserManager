using Manager.Domain.Entities;
using Manager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
namespace Manager.Infra.Context
{
    public class ManagerContext: DbContext
    {
        public ManagerContext(){}
        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        {}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //    optionsBuilder.UseSqlServer(@"Server=localhost, 1433; Database=USERMANAGERAPI; User ID=sa;Password=P@ssw0rd10;");
        // }

        public virtual DbSet<User> Users {get; set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
        }
    }
}
