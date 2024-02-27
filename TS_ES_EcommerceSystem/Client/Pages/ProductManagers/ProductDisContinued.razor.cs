using Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Models;

namespace Client.Pages.ProductManagers
{
    public partial class ProductDisContinued
    {
        private List<Products>? products;
        private int currentPage = 1;
        private int pageSize = 10;
        private string searchTerm = "";
        private int totalPage;
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
            await LoadProducts(currentPage, pageSize, searchTerm);
        }
        protected async Task ReintroduceProduct(int id)
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Bạn chăc chứ?",
                Text = "Bạn sẽ tiến hành xác nhận kinh doanh lại sản phẩm này?",
                Icon = SweetAlertIcon.Info,
                ShowCancelButton = true,
                ConfirmButtonText = "Đòng ý!",
                CancelButtonText = "Không đồng ý."
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                var res = await productServices.DeleteProduct(id, false);


                if (res)
                {
                    await Swal.FireAsync("Okay", "Sản phẩm đã được đưa trở lại danh sách đang kinh doanh.", SweetAlertIcon.Success);
                    await LoadProducts(currentPage, pageSize, searchTerm);
                }
                else
                {
                    await Swal.FireAsync("Lỗi", "Có lỗi xảy ra ở máy chủ.", SweetAlertIcon.Error);
                }
            }
            else if (result.Dismiss == DismissReason.Cancel)
            {
                await Swal.FireAsync("Okay", "Sản phẩm sẽ được giữ lại tại đây :)", SweetAlertIcon.Error);
            }
        }

        protected async Task LoadProducts(int page, int pageSize, string searchTerm)
        {
            (products, totalPage) = await productServices.GetProductsDisContinued(page, pageSize, searchTerm);
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
