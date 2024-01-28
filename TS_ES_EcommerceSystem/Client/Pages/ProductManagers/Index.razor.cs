using Client.Services;
using Microsoft.AspNetCore.Components;
using Models;

namespace Client.Pages.ProductManagers
{
    public partial class Index
    {
        private List<Products>? products;
        private List<Categories>? categories;
        private List<Suppliers>? suppliers;
        private int currentPage = 1;
        private int pageSize = 10;
        private string searchTerm = "";
        private int totalPage;
        int cateID;
        int suppID;

        [SupplyParameterFromForm]
        Products? productModel { get; set; }

        [Inject]
        protected ProductServices productServices { get; set; } = default!;
        [Inject]
        protected CategoryServices categoryServices { get; set; } = default!;
        [Inject]
        protected SuppliersServices suppliersServices { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            productModel ??= new();
            await LoadProducts(currentPage, pageSize, searchTerm);
            await LoadCategories();
            await LoadSuppliers();
        }
        protected async Task CreateProduct()
        {
            var data = new Products
            {
                ProductName = productModel.ProductName,
                CategoryID = productModel.CategoryID,
                SupplierID = productModel.SupplierID,
                QuantityPerUnit = productModel.QuantityPerUnit,
                UnitPrice = productModel.UnitPrice,
                UnitsInStock = productModel.UnitsInStock,
                UnitsOnOrder = 0,
                ReorderLevel = productModel.ReorderLevel,
                Discontinued = true,
            };
            await productServices.CreateProduct(data);

        }

        protected async Task LoadProducts(int page, int pageSize, string searchTerm)
        {
            (products, totalPage) = await productServices.GetProducts(page, pageSize, searchTerm);
        }

        protected async Task LoadCategories()
        {
            categories = await categoryServices.GetCategories();
        }
        protected async Task LoadSuppliers()
        {
            suppliers = await suppliersServices.GetSuppliers();
        }

        protected async Task Search()
        {
            currentPage = 1;
            await Task.Delay(300);
            await LoadProducts(currentPage, pageSize, searchTerm);
        }
        private async Task OnPageChangedAsync(int newPageNumber)
        {
            await Task.Run(() =>
            {
                currentPage = newPageNumber;

            });

            await LoadProducts(currentPage, pageSize, searchTerm);

        }
    }
}
