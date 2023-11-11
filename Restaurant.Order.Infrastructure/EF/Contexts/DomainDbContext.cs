using Microsoft.EntityFrameworkCore;
using Restaurant.Order.Application.OutBox;
using Restaurant.Order.Infrastructure.EF.DomainConfig;
using Restaurant.SharedKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Order.Infrastructure.EF.Contexts
{
    public class DomainDbContext : DbContext
    {
        public DbSet<Domain.Model.Orders.Order> Orders { get; set; }
        public virtual DbSet<OutboxMessage<DomainEvent>> OutboxMessage { get; set; }
        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var orderConfig = new OrderEntityConfig();
            modelBuilder.ApplyConfiguration<Domain.Model.Orders.Order>(orderConfig);
            modelBuilder.ApplyConfiguration<Domain.Model.Orders.OrderLine>(orderConfig);
            modelBuilder.ApplyConfiguration(new OutboxMessageConfig());
        }
    }
}
