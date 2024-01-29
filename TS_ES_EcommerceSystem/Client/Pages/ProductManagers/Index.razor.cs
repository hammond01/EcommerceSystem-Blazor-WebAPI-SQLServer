using Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
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

        [SupplyParameterFromForm]
        Products? productModel { get; set; }

        [Inject]
        protected ProductServices productServices { get; set; } = default!;
        [Inject]
        protected CategoryServices categoryServices { get; set; } = default!;
        [Inject]
        protected SuppliersServices suppliersServices { get; set; } = default!;
        [Inject]
        protected SweetAlertService Swal { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            productModel ??= new();
            await LoadProducts(currentPage, pageSize, searchTerm);
            await LoadCategories();
            await LoadSuppliers();
        }
        protected async Task CreateProduct()
        {

            if (productModel!.ProductID == 0)
            {
                //create new product
                var data = new Products
                {
                    ProductName = productModel!.ProductName,
                    CategoryID = productModel.CategoryID,
                    SupplierID = productModel.SupplierID,
                    QuantityPerUnit = productModel.QuantityPerUnit,
                    UnitPrice = productModel.UnitPrice,
                    UnitsInStock = productModel.UnitsInStock,
                    UnitsOnOrder = 0,
                    ReorderLevel = productModel.ReorderLevel,
                    Discontinued = true,
                };
                var res = await productServices.CreateProduct(data);

                if (res == "Created")
                {
                    await Swal.FireAsync(
                     "Create",
                     "Create product success!",
                     SweetAlertIcon.Success
                     );
                    await LoadProducts(currentPage, pageSize, searchTerm);

                }
                else
                {
                    await Swal.FireAsync(
                     "Cancelled",
                     "Create product is safe :)",
                     SweetAlertIcon.Error
                     );
                }
            }
            else
            {
                // Update product
                var update = await productServices.UpdateProduct(productModel);
                if (update == true)
                {
                    await Swal.FireAsync(
                                            "Updated",
                                            "Product has been Updated.",
                                             SweetAlertIcon.Success
                                        );
                    await LoadProducts(currentPage, pageSize, searchTerm);
                }
                else
                {
                    await Swal.FireAsync(
                                            "Error",
                                            "Product hasn't been Updated.",
                                            SweetAlertIcon.Error
                                        );
                }
            }

        }
        protected async Task DeleteProduct(int id)
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Are you sure?",
                Text = "You will not be able to recover this imaginary file!",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "Yes, delete it!",
                CancelButtonText = "No, keep it"
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                var res = await productServices.DeleteProduct(id);


                if (res == "Deleted")
                {
                    await Swal.FireAsync(
                                             "Deleted",
                                             "Product has been deleted.",
                                             SweetAlertIcon.Success
                                        );
                    await LoadProducts(currentPage, pageSize, searchTerm);
                }
                else
                {
                    await Swal.FireAsync(
                                             "Deleted",
                                             "Product hasn't been deleted.",
                                             SweetAlertIcon.Error
                                        );
                }
            }
            else if (result.Dismiss == DismissReason.Cancel)
            {
                await Swal.FireAsync(
                                          "Cancelled",
                                          "Product is safe :)",
                                          SweetAlertIcon.Error
                                    );
            }
        }
        protected async Task EditProduct(int productID)
        {
            productModel = await productServices.GetProductById(productID);
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
