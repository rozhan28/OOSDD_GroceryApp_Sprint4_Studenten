using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using Grocery.Core.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TestCore
{
    [TestFixture]
    public class GroceryListItemsServiceTests
    {
        private Mock<IGroceryListItemsRepository> _mockRepo;
        private Mock<IProductRepository> _mockProductRepo;
        private GroceryListItemsService _service;
        //test
        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IGroceryListItemsRepository>();
            _mockProductRepo = new Mock<IProductRepository>();
            _service = new GroceryListItemsService(_mockRepo.Object, _mockProductRepo.Object);
        }

        [Test]
        public void GetBestSellingProducts_ReturnsTopX()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetAll()).Returns(new List<GroceryListItem>
{
                new GroceryListItem(1, 1, 1, 2),
                new GroceryListItem(2, 1, 1, 3),
                new GroceryListItem(3, 1, 2, 1)
            });


            _mockProductRepo.Setup(p => p.GetAll()).Returns(new List<Product>
{
                new Product(1, "Banaan", 50),
                new Product(2, "Appel", 30)
            });


            // Act
            var result = _service.GetBestSellingProducts(1);

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Banaan"));
            Assert.That(result.First().NrOfSells, Is.EqualTo(5));
            Assert.That(result.First().Stock, Is.EqualTo(50));
        }
    }
}

