using Exer3.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exer3Tests
{
    public class ItemTests
    {
        [Fact]
        public void SufficientStock_pass()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<Ex3DbContext>().UseSqlite(connection).Options;

            using (var context = new Ex3DbContext(options))
            {
                context.Database.EnsureCreated();
                context.Items.AddRange(
                    new Item { ItemID = 1, ItemName = "Coffee Mug", Size = null, Stock = 50},
                    new Item { ItemID = 2, ItemName = "Table", Size = null, Stock = 15});
                context.SaveChanges();

                var service = new DALservice(context);

                int itemId = 1;
                int quantity = 30;
                bool res = service.SufficientStock(itemId, quantity);
                Assert.True(res);

                quantity = 500;
                res = service.SufficientStock(itemId, quantity);
                Assert.False(res);

                itemId = 1;
                string itemname = "Coffee Mug";
                string size = null;
                int stock = 50;
                var item = service.Items.FirstOrDefault(i => i.ItemID == itemId);
                Assert.NotNull(item);
                Assert.Equal(itemId, item.ItemID);
                Assert.Equal(itemname, item.ItemName);
                Assert.Equal(size, item.Size);
                Assert.Equal(stock, item.Stock);

                itemId = 50;
                item = service.Items.FirstOrDefault(i => i.ItemID == itemId);
                Assert.Null(item);
            }
            
        }
    }
}
