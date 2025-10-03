namespace Grocery.Core.Models
{
    // Item in een boodschappenlijst
    public class GroceryListItem : Model
    {
        private int _amount;

        public int GroceryListId { get; set; }
        public int ProductId { get; set; }

        // Hoeveelheid van het product, notificeert bij wijziging
        public int Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }

        // Bijbehorend product
        public Product Product { get; set; } = new(0, "None", 0);

        public GroceryListItem(int id, int groceryListId, int productId, int amount)
            : base(id, "")
        {
            GroceryListId = groceryListId;
            ProductId = productId;
            _amount = amount;
        }
    }
}
