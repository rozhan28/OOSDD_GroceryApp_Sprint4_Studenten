﻿
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class BestSellingProductsViewModel : BaseViewModel
    {
        private readonly IGroceryListItemsService _groceryListItemsService;

        public ObservableCollection<BestSellingProducts> Products { get; set; } = [];

        public BestSellingProductsViewModel(IGroceryListItemsService groceryListItemsService)
        {
            _groceryListItemsService = groceryListItemsService;
            Load();
        }

        public override void Load()
        {
            Products.Clear();
            foreach (var item in _groceryListItemsService.GetBestSellingProducts())
            {
                Products.Add(item);
            }
        }

        public override void OnAppearing()
        {
            Load();
        }

        public override void OnDisappearing()
        {
            Products.Clear();
        }
    }
}
