using Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Models;

namespace Client.Pages.ProductManagers
{
    public partial class Index
    {
        private List<Models.Products>? products;
        private List<Categories>? categories;
        private List<Suppliers>? suppliers;
        private int currentPage = 1;
        private int pageSize = 10;
        private string searchTerm = "";
        private int totalPage;
        private bool editFunction;

        [SupplyParameterFromForm]
        Models.Products? productModel { get; set; }

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
                var data = new Models.Products
                {
                    ProductName = productModel!.ProductName,
                    CategoryID = productModel.CategoryID,
                    SupplierID = productModel.SupplierID,
                    QuantityPerUnit = productModel.QuantityPerUnit,
                    UnitPrice = productModel.UnitPrice,
                    UnitsInStock = productModel.UnitsInStock,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
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
                var update = await productServices.UpdateProduct(productModel);
                if (update)
                {
                    await Swal.FireAsync("Chỉnh sửa", "Chỉnh sửa thông tin sản phẩm thành công.", SweetAlertIcon.Success);
                    await LoadProducts(currentPage, pageSize, searchTerm);
                }
                else
                {
                    await Swal.FireAsync("Lỗi", "Có lỗi xảy ra ở phía máy chủ.", SweetAlertIcon.Error);
                }
            }

        }
        protected async Task DeleteProduct(int id)
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Hmmm?",
                Text = "Bạn có chắc là không còn kinh doanh sản phẩm này nữa!",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "Xác nhận",
                CancelButtonText = "Giữ nó lại."
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                var res = await productServices.DeleteProduct(id, true);

                if (res)
                {
                    await Swal.FireAsync("Thành công", "Sản phẩm đã được đưa tới danh sách không còn kinh doanh.", SweetAlertIcon.Success);
                    await LoadProducts(currentPage, pageSize, searchTerm);
                }
                else
                {
                    await Swal.FireAsync("Lỗi", "Có lỗi xảy ra ở máy chủ.", SweetAlertIcon.Error);
                }
            }
            else if (result.Dismiss == DismissReason.Cancel)
            {
                await Swal.FireAsync("Hủy", "Sản phẩm sẽ được giữ lại :)", SweetAlertIcon.Error);
            }
        }
        protected async Task EditProduct(int productID)
        {
            editFunction = true;
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
        private void ClearData()
        {
            editFunction = false;
            productModel = new();
        }
        private void CreateFunction()
        {
            editFunction = false;
            productModel = new();
        }
    }
}
