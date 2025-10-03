using NUnit.Framework;
using Grocery.Core.Models;
using Grocery.Core.Services;
using Grocery.Core.Interfaces.Repositories;
using Moq;
using System.Collections.Generic;

namespace Grocery.Tests
{
    public class BoughtProductsServiceTests
    {
        [Test]
        public void Get_ReturnsAllClientsWhoBoughtProduct()
        {
            // Arrange
            int productId = 1;

            var clients = new List<Client>
            {//test
                new Client(1, "Alice", "alice@mail.com", "password") { Role = Role.None },
                new Client(2, "Bob", "bob@mail.com", "password") { Role = Role.None },
                new Client(3, "Charlie", "charlie@mail.com", "password") { Role = Role.Admin }
            };
            var clientRepoMock = new Mock<IClientRepository>();
            clientRepoMock.Setup(r => r.GetAll()).Returns(clients);

            var products = new List<Product>
            {
                new Product(1, "Apple", 10),
                new Product(2, "Banana", 20)
            };
            var productRepoMock = new Mock<IProductRepository>();
            productRepoMock.Setup(r => r.GetAll()).Returns(products);

            var groceryLists = new List<GroceryList>
            {
               new GroceryList(1, "List1", DateOnly.FromDateTime(DateTime.Today), "Red", 1),
               new GroceryList(2, "List2", DateOnly.FromDateTime(DateTime.Today), "Blue", 2),

            };
            var groceryListRepoMock = new Mock<IGroceryListRepository>();
            groceryListRepoMock.Setup(r => r.GetAll()).Returns(groceryLists);

            var groceryListItems = new List<GroceryListItem>
            {
                new GroceryListItem(1, 1, 1, 3), 
                new GroceryListItem(2, 2, 2, 1), 
                new GroceryListItem(3, 2, 1, 2)  
            };
            var groceryListItemsRepoMock = new Mock<IGroceryListItemsRepository>();
            groceryListItemsRepoMock.Setup(r => r.GetAll()).Returns(groceryListItems);

            var service = new BoughtProductsService(
                groceryListItemsRepoMock.Object,
                groceryListRepoMock.Object,
                clientRepoMock.Object,
                productRepoMock.Object
            );

            // Act
            var result = service.Get(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count); 
            Assert.IsTrue(result.Exists(p => p.ClientName == "Alice" && p.GroceryListName == "List1"));
            Assert.IsTrue(result.Exists(p => p.ClientName == "Bob" && p.GroceryListName == "List2"));
        }
    }
}

