using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Exer3.Models
{
    public class Ex3DbContext : DbContext
    { 
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Users> Users { get; set; }


        public Ex3DbContext(DbContextOptions<Ex3DbContext> options) : base(options) { }
    }
}
