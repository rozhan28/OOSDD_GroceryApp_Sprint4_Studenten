
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Grocery.Core.Services
{
    public class BoughtProductsService : IBoughtProductsService
    {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListRepository _groceryListRepository;

        public BoughtProductsService(
            IGroceryListItemsRepository groceryListItemsRepository,
            IGroceryListRepository groceryListRepository,
            IClientRepository clientRepository,
            IProductRepository productRepository)
        {
            _groceryListItemsRepository = groceryListItemsRepository;
            _groceryListRepository = groceryListRepository;
            _clientRepository = clientRepository;
            _product_repository_check(productRepository);
            _productRepository = productRepository;
        }

        private void _product_repository_check(IProductRepository _p) { }

        public List<BoughtProducts> Get(int? productId)
        {
            var allItems = _groceryListItemsRepository.GetAll();

            var filtered = productId.HasValue
                ? allItems.Where(i => i.ProductId == productId.Value).ToList()
                : allItems.ToList();

            var allProducts = _productRepository.GetAll().ToDictionary(p => p.Id);
            var allLists = _groceryListRepository.GetAll().ToDictionary(l => l.Id);
            var allClients = _clientRepository.GetAll().ToDictionary(c => c.Id);

            var result = new List<BoughtProducts>();

            foreach (var item in filtered)
            {
                var product = allProducts.ContainsKey(item.ProductId)
                    ? allProducts[item.ProductId]
                    : new Product(0, "Onbekend product", 0);

                var list = allLists.ContainsKey(item.GroceryListId)
                    ? allLists[item.GroceryListId]
                    : new GroceryList(0, "Onbekende lijst", DateOnly.FromDateTime(System.DateTime.Now), "", 0);

                var client = allClients.ContainsKey(list.ClientId)
                    ? allClients[list.ClientId]
                    : new Client(0, "Onbekende klant", "", "");

                var bp = new BoughtProducts(
                    client.Id,
                    client.Name,
                    list.Id,
                    list.Name,
                    product.Id,
                    product.Name,
                    item.Amount
                );

                result.Add(bp);
            }

            return result.OrderBy(r => r.ClientName).ThenBy(r => r.GroceryListName).ToList();
        }
    }
}
