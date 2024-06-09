using Microsoft.EntityFrameworkCore;
using Models.People;

namespace AndreVeiculosAPI.Data
{
    public class AndreVeiculosAPIContext : DbContext
    {
        public AndreVeiculosAPIContext(DbContextOptions<AndreVeiculosAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Models.People.Customer> Customer { get; set; } = default!;

        public DbSet<Models.People.Employee>? Employee { get; set; } = default!;

        public DbSet<Models.Profitable.Payment>? Payment { get; set; } = default!;

        public DbSet<Models.Cars.Operation>? Operation { get; set; } = default!;

        public DbSet<Models.Profitable.Sale>? Sale { get; set; } = default!;

        public DbSet<Models.Cars.Car>? Car { get; set; } = default!;

        public DbSet<Models.Profitable.Purchase>? Purchase { get; set; } = default!;

        public DbSet<Models.Cars.CarOperation>? CarOperation { get; set; } = default!;

        public DbSet<Models.People.JobTitle>? JobTitle { get; set; }
    }
}
