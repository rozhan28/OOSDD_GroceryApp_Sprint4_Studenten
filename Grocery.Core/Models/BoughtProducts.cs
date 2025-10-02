
namespace Grocery.Core.Models
{
    public class BoughtProducts
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;

        public int GroceryListId { get; set; }
        public string GroceryListName { get; set; } = string.Empty;

        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;

        public int Amount { get; set; }

        public BoughtProducts() { }

        public BoughtProducts(int clientId, string clientName, int groceryListId, string groceryListName, int productId, string productName, int amount)
        {
            ClientId = clientId;
            ClientName = clientName;
            GroceryListId = groceryListId;
            GroceryListName = groceryListName;
            ProductId = productId;
            ProductName = productName;
            Amount = amount;
        }
    }
}
