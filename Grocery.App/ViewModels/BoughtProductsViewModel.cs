using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class BoughtProductsViewModel : BaseViewModel
    {
        private readonly IBoughtProductsService _boughtProductsService;

        [ObservableProperty]
        private Product? selectedProduct;

        public ObservableCollection<BoughtProducts> BoughtProductsList { get; } = new ObservableCollection<BoughtProducts>();
        public ObservableCollection<Product> Products { get; }

        public BoughtProductsViewModel(IBoughtProductsService boughtProductsService, IProductService productService)
        {
            _boughtProductsService = boughtProductsService;
            Products = new ObservableCollection<Product>(productService.GetAll());
        }

        // Wordt aangeroepen wanneer SelectedProduct verandert
        partial void OnSelectedProductChanged(Product? oldValue, Product newValue)
        {
            BoughtProductsList.Clear();

            if (newValue == null) return;

            var boughtProducts = _boughtProductsService.Get(newValue.Id);
            foreach (var bp in boughtProducts)
            {
                BoughtProductsList.Add(bp);
            }
        }

        // RelayCommand voor het selecteren van een nieuw product
        [RelayCommand]
        public void NewSelectedProduct(Product product)
        {
            SelectedProduct = product;
        }
    }
}

