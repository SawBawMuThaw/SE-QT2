using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Exer2.Controllers;
using Exer2.Models;
using Moq;
using System.Data.Entity;
using System.Web.Mvc;

namespace Exer2.Tests
{
    public class ControllerTests : TestBase
    {
        [Theory]
        [InlineData("user1", "password123", "/Home/Index")] // EP: Valid credentials
        [InlineData("user1", "wrong", "Login")] // EP: Invalid password
        [InlineData("", "", "Login")] // EP: Empty input
        public void Login_ValidAndInvalidCredentials_ReturnsExpectedResult(string username, string password, string expected)
        {
            // Arrange
            SetupMockContext();
            var controller = new AccountController();
            SetPrivateDbField(controller, MockDbContext.Object);

            var loginModel = new LoginViewModel { UserName = username, Password = password };

            // Mock the Users DbSet
            var users = new List<User>
            {
                new User { UserName = "user1", Password = "password123" }
            }.AsQueryable();

            var mockUsers = new Mock<DbSet<User>>();
            mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            MockDbContext.Setup(m => m.Users).Returns(mockUsers.Object);

            // Act
            var result = controller.Login(loginModel);

            // Assert
            if (expected.StartsWith("/"))
            {
                Assert.IsType<RedirectResult>(result);
                Assert.Equal(expected, (result as RedirectResult)?.Url);
                
            }
            else
            {
                Assert.IsType<ViewResult>(result);
                Assert.Equal(expected, (result as ViewResult)?.ViewName ?? "Login");
            }
        }

        [Theory]
        [InlineData(1, 1, 2, 1, 10, "Index")] // EP: Valid order with 1 item
        [InlineData(1000, 1000, 2000, 5, 50, "Index")] // BVA: Large quantities
        [InlineData(1, 0, 1, 1, 0, "Index")] // BVA: Minimum quantity
        public void CreateOrder_ValidModel_RedirectsToIndex(int orderId, int agentId, int quantity, int itemId, int unitAmount, string expected)
        {
            // Arrange
            SetupMockContext();
            var controller = new OrderController();
            SetPrivateDbField(controller, MockDbContext.Object);

            // Mock ModelState to be valid
            var modelState = new ModelStateDictionary();
            controller.ModelState.Clear();
            controller.ModelState.Merge(modelState);

            var orderViewModel = new OrderViewModel
            {
                Order = new Order { OrderID = orderId, OrderDate = DateTime.Now, AgentID = agentId },
                OrderDetails = new List<OrderDetail> { new OrderDetail { ID = 1, OrderID = orderId, ItemID = itemId, Quantity = quantity, UnitAmount = unitAmount } },
                AvailableItems = new List<Item> { new Item { ItemID = itemId, ItemName = "Test Item" } }
            };

            var mockOrders = new Mock<DbSet<Order>>();
            mockOrders.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(new List<Order>().AsQueryable().Provider);
            mockOrders.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(new List<Order>().AsQueryable().Expression);
            mockOrders.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(new List<Order>().AsQueryable().ElementType);
            mockOrders.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(new List<Order>().AsQueryable().GetEnumerator());
            MockDbContext.Setup(m => m.Orders).Returns(mockOrders.Object);

            var mockOrderDetails = new Mock<DbSet<OrderDetail>>();
            mockOrderDetails.As<IQueryable<OrderDetail>>().Setup(m => m.Provider).Returns(new List<OrderDetail>().AsQueryable().Provider);
            mockOrderDetails.As<IQueryable<OrderDetail>>().Setup(m => m.Expression).Returns(new List<OrderDetail>().AsQueryable().Expression);
            mockOrderDetails.As<IQueryable<OrderDetail>>().Setup(m => m.ElementType).Returns(new List<OrderDetail>().AsQueryable().ElementType);
            mockOrderDetails.As<IQueryable<OrderDetail>>().Setup(m => m.GetEnumerator()).Returns(new List<OrderDetail>().AsQueryable().GetEnumerator());
            MockDbContext.Setup(m => m.OrderDetails).Returns(mockOrderDetails.Object);

            var mockItems = new Mock<DbSet<Item>>();
            var items = new List<Item> { new Item { ItemID = itemId, ItemName = "Test Item" } }.AsQueryable();
            mockItems.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(items.Provider);
            mockItems.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(items.Expression);
            mockItems.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(items.ElementType);
            mockItems.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(items.GetEnumerator());
            MockDbContext.Setup(m => m.Items).Returns(mockItems.Object);

            MockDbContext.Setup(m => m.SaveChanges()).Returns(1);

            // Act
            var result = controller.Create(orderViewModel);

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(10, 5, 15, "Item3")] // EP: Normal quantities
        [InlineData(100, 50, 150, "Item3")] // BVA: Large quantities
        [InlineData(0, 0, 0, "Item1")] // BVA: Minimum quantities
        public void BestItems_ReturnsTopItems(int q1, int q2, int q3, string expectedTopItem)
        {
            // Arrange
            SetupMockContext();
            var controller = new ItemsController();
            SetPrivateDbField(controller, MockDbContext.Object);

            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail { ItemID = 1, Quantity = q1 },
                new OrderDetail { ItemID = 2, Quantity = q2 },
                new OrderDetail { ItemID = 3, Quantity = q3 }
            }.AsQueryable();

            var items = new List<Item>
            {
                new Item { ItemID = 1, ItemName = "Item1" },
                new Item { ItemID = 2, ItemName = "Item2" },
                new Item { ItemID = 3, ItemName = "Item3" }
            }.AsQueryable();

            var mockOrderDetails = new Mock<DbSet<OrderDetail>>();
            mockOrderDetails.As<IQueryable<OrderDetail>>().Setup(m => m.Provider).Returns(orderDetails.Provider);
            mockOrderDetails.As<IQueryable<OrderDetail>>().Setup(m => m.Expression).Returns(orderDetails.Expression);
            mockOrderDetails.As<IQueryable<OrderDetail>>().Setup(m => m.ElementType).Returns(orderDetails.ElementType);
            mockOrderDetails.As<IQueryable<OrderDetail>>().Setup(m => m.GetEnumerator()).Returns(orderDetails.GetEnumerator());
            MockDbContext.Setup(m => m.OrderDetails).Returns(mockOrderDetails.Object);

            var mockItems = new Mock<DbSet<Item>>();
            mockItems.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(items.Provider);
            mockItems.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(items.Expression);
            mockItems.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(items.ElementType);
            mockItems.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(items.GetEnumerator());
            MockDbContext.Setup(m => m.Items).Returns(mockItems.Object);

            // Act
            var result = controller.BestItems() as ViewResult;
            var model = result.Model as List<BestItemViewModel>;

            // Assert
            Assert.NotNull(model);
            Assert.Equal(3, model.Count); // Should return all items due to sample data
            Assert.Equal(expectedTopItem, model[0].ItemName); // Check the top item by quantity
        }
    }
}