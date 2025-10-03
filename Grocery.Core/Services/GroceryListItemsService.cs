using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    // Service voor het beheren van GroceryListItems
    public class GroceryListItemsService : IGroceryListItemsService
    {
        private readonly IGroceryListItemsRepository _groceriesRepository;
        private readonly IProductRepository _productRepository;

        public GroceryListItemsService(
            IGroceryListItemsRepository groceriesRepository,
            IProductRepository productRepository)
        {
            _groceriesRepository = groceriesRepository;
            _productRepository = productRepository;
        }

        // Haal alle grocery items op en vul product info
        public List<GroceryListItem> GetAll()
        {
            var groceryListItems = _groceriesRepository.GetAll();
            FillService(groceryListItems);
            return groceryListItems;
        }

        // Haal alle items op voor een specifieke boodschappenlijst
        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId)
        {
            var groceryListItems = _groceriesRepository
                .GetAll()
                .Where(g => g.GroceryListId == groceryListId)
                .ToList();

            FillService(groceryListItems);
            return groceryListItems;
        }

        public GroceryListItem Add(GroceryListItem item) => _groceriesRepository.Add(item);

        public GroceryListItem? Delete(GroceryListItem item)
        {
            // nog niet geïmplementeerd
            throw new NotImplementedException(); 
        }

        public GroceryListItem? Get(int id) => _groceriesRepository.Get(id);

        public GroceryListItem? Update(GroceryListItem item) => _groceriesRepository.Update(item);

        // Top X best verkopende producten berekenen
        public List<BestSellingProducts> GetBestSellingProducts(int topX = 5)
        {
            var allItems = _groceriesRepository.GetAll();
            var allProducts = _productRepository.GetAll().ToDictionary(p => p.Id);

            var bestSelling = allItems
                .GroupBy(i => i.ProductId)
                .Select(g =>
                {
                    var productInfo = allProducts.ContainsKey(g.Key)
                        ? allProducts[g.Key]
                        : new Product(0, "Onbekend product", 0);

                    return new
                    {
                        ProductId = g.Key,
                        productInfo.Name,
                        productInfo.Stock,
                        NrOfSells = g.Sum(i => i.Amount)
                    };
                })
                .OrderByDescending(p => p.NrOfSells)
                .Take(topX)
                .Select((p, index) => new BestSellingProducts(
                    p.ProductId,
                    p.Name,
                    p.Stock,
                    p.NrOfSells,
                    index + 1
                ))
                .ToList();

            return bestSelling;
        }

        // Vul productinformatie in de items
        private void FillService(List<GroceryListItem> groceryListItems)
        {
            foreach (var g in groceryListItems)
            {
                g.Product = _productRepository.Get(g.ProductId) ?? new Product(0, "", 0);
            }
        }
    }
}

