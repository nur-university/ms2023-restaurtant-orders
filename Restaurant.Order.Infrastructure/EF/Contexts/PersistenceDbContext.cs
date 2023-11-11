using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Infrastructure.EF.Contexts
{
    internal class PersistenceDbContext : DbContext
    {
        public DbSet<PersistenceModel.OrderPersistenceModel> Order { get; set; }
        public DbSet<PersistenceModel.OutboxPersistenceModel> Outbox { get; set; }

        public PersistenceDbContext(DbContextOptions<PersistenceDbContext> options) : base(options)
        {
        }
    }
}
