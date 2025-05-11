using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Exer1.Tests
{
    public class ItemRepoTest
    {
        private ItemRepo repo { get; set; }
        
        public ItemRepoTest()
        {
            repo = new ItemRepo();
        }

        [Fact]
        public void SufficientStock_pass()
        {
            int itemId = 5;
            int quantity = 30;
            bool res = repo.sufficientStock(itemId, quantity);
            Assert.True(res);
        }

        [Fact]
        public void SufficientStock_fail()
        {
            int itemId = 5;
            int quantity = 500;
            bool res = repo.sufficientStock(itemId, quantity);
            Assert.False(res);
        }

        [Fact]
        public void SufficientStock_fail2()
        {
            int itemId = 50;
            int quantity = 500;
            bool res = repo.sufficientStock(itemId, quantity);
            Assert.False(res);
        }

        [Fact]
        public void GetItem_pass()
        {
            int itemId = 1;
            string itemname = "Coffee Mug";
            string size = null;
            int stock = 130;
            var item = repo.GetItem(itemId);
            Assert.NotNull(item);
            Assert.Equal(itemId, item.ItemID);
            Assert.Equal(itemname, item.ItemName);
            Assert.Equal(size, item.Size);
            Assert.Equal(stock, item.Stock);
        }

        [Fact]
        public void GetItem_fail()
        {
            int itemId = 50; // item does not exist
            var item = repo.GetItem(itemId);
            Assert.Null(item);
        }

    }
}
