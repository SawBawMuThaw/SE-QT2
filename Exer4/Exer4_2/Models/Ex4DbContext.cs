using Microsoft.EntityFrameworkCore;

namespace Exer4_2.Models
{
    public class Ex4DbContext : DbContext
    {
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<User> Users { get; set; }

        public Ex4DbContext(DbContextOptions<Ex4DbContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>().HasData(
                new Agent { AgentID = 1, AgentName = "Alice Smith", Address = "10 Pine St" },
                new Agent { AgentID = 2, AgentName = "Bob Johnson", Address = "22 Oak Ave" },
                new Agent { AgentID = 3, AgentName = "Charlie Brown", Address = "33 Elm Rd" },
                new Agent { AgentID = 4, AgentName = "Diana Lee", Address = "44 Willow Ln" },
                new Agent { AgentID = 5, AgentName = "Ethan Davis", Address = "55 Maple Dr" },
                new Agent { AgentID = 6, AgentName = "Fiona Wilson", Address = "66 Birch Ct" },
                new Agent { AgentID = 7, AgentName = "George Moore", Address = "77 Cedar Blvd" },
                new Agent { AgentID = 8, AgentName = "Hannah Taylor", Address = "88 Spruce Way" },
                new Agent { AgentID = 9, AgentName = "Ian Clark", Address = "99 Redwood Pl" },
                new Agent { AgentID = 10, AgentName = "Julia Lewis", Address = "101 Oak St" },
                new Agent { AgentID = 11, AgentName = "Kevin Hall", Address = "112 Pine Ave" },
                new Agent { AgentID = 12, AgentName = "Linda Allen", Address = "123 Elm Rd" },
                new Agent { AgentID = 13, AgentName = "Michael Young", Address = "134 Willow Ln" },
                new Agent { AgentID = 14, AgentName = "Nancy King", Address = "145 Maple Dr" },
                new Agent { AgentID = 15, AgentName = "Oliver Wright", Address = "156 Birch Ct" }
            );

            modelBuilder.Entity<Item>().HasData(
                new Item { ItemID = 1, ItemName = "T-Shirt", Size = "M", Stock = 50 },
                new Item { ItemID = 2, ItemName = "Jeans", Size = "32", Stock = 30 },
                new Item { ItemID = 3, ItemName = "Mug", Size = null, Stock = 100 },
                new Item { ItemID = 4, ItemName = "Book", Size = null, Stock = 75 },
                new Item { ItemID = 5, ItemName = "Pen", Size = null, Stock = 200 },
                new Item { ItemID = 6, ItemName = "Sweater", Size = "S", Stock = 25 },
                new Item { ItemID = 7, ItemName = "Belt", Size = "34", Stock = 40 },
                new Item { ItemID = 8, ItemName = "Backpack", Size = null, Stock = 60 },
                new Item { ItemID = 9, ItemName = "Dress", Size = "8", Stock = 35 },
                new Item { ItemID = 10, ItemName = "Notebook", Size = null, Stock = 120 },
                new Item { ItemID = 11, ItemName = "Gloves", Size = "M", Stock = 70 },
                new Item { ItemID = 12, ItemName = "Scarf", Size = "One Size", Stock = 55 },
                new Item { ItemID = 13, ItemName = "Water Bottle", Size = null, Stock = 90 },
                new Item { ItemID = 14, ItemName = "Pants", Size = "30", Stock = 32 },
                new Item { ItemID = 15, ItemName = "Keyboard", Size = null, Stock = 45 }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, Username = "alice123", Email = "alice@example.com", Password = "P@$$wOrd1" },
                new User { UserID = 2, Username = "bob_the_builder", Email = "bob@example.org", Password = "SecurePwd2!" },
                new User { UserID = 3, Username = "charlie77", Email = "charlie@test.net", Password = "MySecret3#" },
                new User { UserID = 4, Username = "diana.lee", Email = "diana@sample.com", Password = "StrongPass4$" },
                new User { UserID = 5, Username = "ethan_dev", Email = "ethan@code.com", Password = "CodeMaster5%" },
                new User { UserID = 6, Username = "fiona_art", Email = "fiona@art.net", Password = "ArtLover6^" },
                new User { UserID = 7, Username = "george_sci", Email = "george@science.org", Password = "Science7&" },
                new User { UserID = 8, Username = "hannah_read", Email = "hannah@books.com", Password = "BookWorm8*" },
                new User { UserID = 9, Username = "ian_gamer", Email = "ian@games.net", Password = "LevelUp9(" },
                new User { UserID = 10, Username = "julia_travel", Email = "julia@travel.org", Password = "WorldTrip10)" },
                new User { UserID = 11, Username = "kevin_music", Email = "kevin@music.com", Password = "TuneTime11!" },
                new User { UserID = 12, Username = "linda_bake", Email = "linda@bake.net", Password = "SweetTreat12#" },
                new User { UserID = 13, Username = "michael_photo", Email = "michael@photo.org", Password = "SnapShot13$" },
                new User { UserID = 14, Username = "nancy_garden", Email = "nancy@garden.com", Password = "GreenThumb14%" },
                new User { UserID = 15, Username = "oliver_tech", Email = "oliver@tech.net", Password = "FutureIsNow15^" }
                );

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderID = 1, OrderDate = Convert.ToDateTime("3/7/2024"), AgentID = 8 },
                new Order { OrderID = 2, OrderDate = Convert.ToDateTime("7/3/2024"), AgentID = 8 },
                new Order { OrderID = 3, OrderDate = Convert.ToDateTime("23/1/2024"), AgentID = 8 },
                new Order { OrderID = 4, OrderDate = Convert.ToDateTime("11/9/2024"), AgentID = 8 },
                new Order { OrderID = 5, OrderDate = Convert.ToDateTime("6/5/2024"), AgentID = 8 }
                );

            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail { ID = 1, OrderID = 1, ItemID = 1, Quantity = 1, UnitAmount = 10 },
                new OrderDetail { ID = 2, OrderID = 1, ItemID = 2, Quantity = 2, UnitAmount = 20 },
                new OrderDetail { ID = 3, OrderID = 2, ItemID = 3, Quantity = 1, UnitAmount = 30 },
                new OrderDetail { ID = 4, OrderID = 2, ItemID = 4, Quantity = 3, UnitAmount = 15 },
                new OrderDetail { ID = 5, OrderID = 3, ItemID = 5, Quantity = 1, UnitAmount = 25 },
                new OrderDetail { ID = 6, OrderID = 3, ItemID = 6, Quantity = 2, UnitAmount = 35 },
                new OrderDetail { ID = 7, OrderID = 4, ItemID = 7, Quantity = 1, UnitAmount = 45 },
                new OrderDetail { ID = 8, OrderID = 4, ItemID = 8, Quantity = 4, UnitAmount = 12 },
                new OrderDetail { ID = 9, OrderID = 5, ItemID = 9, Quantity = 1, UnitAmount = 22 },
                new OrderDetail { ID = 10, OrderID = 5, ItemID = 10, Quantity = 2, UnitAmount = 32 },
                new OrderDetail { ID = 11, OrderID = 1, ItemID = 11, Quantity = 1, UnitAmount = 42 },
                new OrderDetail { ID = 12, OrderID = 2, ItemID = 12, Quantity = 3, UnitAmount = 18 },
                new OrderDetail { ID = 13, OrderID = 3, ItemID = 13, Quantity = 1, UnitAmount = 28 },
                new OrderDetail { ID = 14, OrderID = 4, ItemID = 14, Quantity = 2, UnitAmount = 38 },
                new OrderDetail { ID = 15, OrderID = 5, ItemID = 15, Quantity = 1, UnitAmount = 48 }
                );

        }
    }
}
