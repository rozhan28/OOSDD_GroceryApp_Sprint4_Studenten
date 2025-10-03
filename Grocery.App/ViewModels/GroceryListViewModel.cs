using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Grocery.App.ViewModels
{
    public partial class GroceryListViewModel : BaseViewModel
    {
        private readonly IGroceryListService _groceryListService;
        private readonly IClientService _clientService;

        public ObservableCollection<GroceryList> GroceryLists { get; private set; }

        // Huidige ingelogde client
        public Client Client { get; private set; }

        public GroceryListViewModel(IGroceryListService groceryListService, IClientService clientService)
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;
            _clientService = clientService;

            Client = _clientService.Get(3) ?? new Client(0, "Onbekend", "", "");

            GroceryLists = new ObservableCollection<GroceryList>(_groceryListService.GetAll());
        }

        // Command om een boodschappenlijst te selecteren en navigeren naar de items view
        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            var parameters = new Dictionary<string, object>
            {
                { nameof(GroceryList), groceryList }
            };
            await Shell.Current.GoToAsync($"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}", true, parameters);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            GroceryLists = new ObservableCollection<GroceryList>(_groceryListService.GetAll());
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }

        // Command om aangekochte producten te tonen, alleen voor admin
        [RelayCommand]
        public async Task ShowBoughtProducts()
        {
            if (Client?.Role == Role.Admin)
            {
                await Shell.Current.GoToAsync(nameof(Views.BoughtProductsView));
            }
        }
    }
}

