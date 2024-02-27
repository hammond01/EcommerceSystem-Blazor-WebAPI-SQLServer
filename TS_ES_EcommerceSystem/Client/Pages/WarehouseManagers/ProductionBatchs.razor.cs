using Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Models;
using Models.ResponseModel;
using Models.WarehouseModel;
using System.Data.SqlTypes;

namespace Client.Pages.WarehouseManagers
{
    public partial class ProductionBatchs
    {
        #region Variable
        private bool IsButtonDisabled = true;
        private bool addProductionBatch = false;
        private string CheckManufactureDate { get; set; } = default!;
        private string CheckExpiryDate { get; set; } = default!;
        #endregion
        #region ListData
        private List<ProductBathResponse> productionBatches { get; set; } = default!;
        private List<Products>? products { get; set; } = default!;
        private List<Units>? units { get; set; } = default!;
        #endregion
        #region Inject
        [Inject]
        protected ProductServices productServices { get; set; } = default!;
        [Inject]
        protected ProductionBatchServices productionBatchServices { get; set; } = default!;
        [Inject]
        protected UnitServices unitServices { get; set; } = default!;
        [Inject]
        protected SweetAlertService Swal { get; set; } = default!;
        #endregion
        [SupplyParameterFromForm]
        ProductBathResponse? productionBatchModel { get; set; }
        protected override async Task OnInitializedAsync()
        {
            productionBatchModel ??= new ProductBathResponse();
            productionBatchModel.ExpiryDate = DateTime.Now;
            productionBatchModel.ManufactureDate = DateTime.Now;
            await GetProductBatchs();
        }
        #region Create, Edit and Delete
        protected async Task CreateProductionBatch()
        {
            if (productionBatchModel!.ProductionBatchID == 0)
            {
                var data = new ProductionBatch
                {
                    ProductID = productionBatchModel.ProductID,
                    UnitID = productionBatchModel.UnitID,
                    ProductionBatchName = "",
                    Quantity = productionBatchModel.Quantity,
                    PriceOfBatch = productionBatchModel.PriceOfBatch,
                    ManufactureDate = productionBatchModel.ManufactureDate,
                    ExpiryDate = productionBatchModel.ExpiryDate,
                };
                var res = await productionBatchServices.CreateProductionBatch(data);
                if (res)
                {
                    await Swal.FireAsync("Create", "Created!", SweetAlertIcon.Success);
                    await GetProductBatchs();
                }
                else
                {
                    await Swal.FireAsync("Cancelled", "Create is safe :)", SweetAlertIcon.Error);
                }
            }
            else
            {
                var update = await productionBatchServices.UpdateProductionBatch(productionBatchModel);
                if (update == true)
                {
                    await Swal.FireAsync("Updated", "Updated.", SweetAlertIcon.Success);
                    await GetProductBatchs();
                }
                else
                {
                    await Swal.FireAsync("Error", "Error", SweetAlertIcon.Error);
                }
            }
        }
        protected async Task DeleteProductbatch(int id)
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Bạn có chắn chắn xóa chứ?",
                Text = "You will not be able to recover this imaginary file!",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "Yes, delete it!",
                CancelButtonText = "No, keep it"
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                var res = await productionBatchServices.DeleteProductionBatch(id);


                if (res)
                {
                    await Swal.FireAsync("Deleted", "Product has been deleted.", SweetAlertIcon.Success);
                    await GetProductBatchs();
                }
                else
                {
                    await Swal.FireAsync("Deleted", "Product hasn't been deleted.", SweetAlertIcon.Error);
                }
            }
            else if (result.Dismiss == DismissReason.Cancel)
            {
                await Swal.FireAsync("Cancelled", "Product is safe :)", SweetAlertIcon.Error);
            }
        }
        #endregion
        #region Get endpoint
        protected async Task GetProductBatchByProductionBatchID(int id)
        {
            productionBatchModel = await productionBatchServices.GetProductionBatchById(id);
            IsButtonDisabled = false;
        }

        protected async Task GetProductBatchs()
        {
            productionBatches = await productionBatchServices.GetProductionBatchs();
            products = await productServices.GetProductsInProductionBatch();
            units = await unitServices.GetUnits();
        }
        private void GetProducts()
        {
            addProductionBatch = true;
            IsButtonDisabled = false;
        }
        #endregion

        #region Validate
        private void HandleManufactureDateChange(ChangeEventArgs e)
        {
            if (!DateTime.TryParse(e.Value!.ToString(), out DateTime selectedDate) ||
                selectedDate < SqlDateTime.MinValue.Value || selectedDate > SqlDateTime.MaxValue.Value)
            {
                CheckManufactureDate = "Invalid date. Please select a date within the valid range.";
                IsButtonDisabled = true;
            }
            else if (selectedDate > productionBatchModel!.ExpiryDate)
            {
                CheckManufactureDate = "Manufacture date cannot be greater than expiry date.";
                IsButtonDisabled = true;
            }
            else
            {
                productionBatchModel!.ManufactureDate = selectedDate;
                CheckManufactureDate = "";
                IsButtonDisabled = false;
            }
        }
        private void HandleExpiryDateChange(ChangeEventArgs e)
        {
            if (!DateTime.TryParse(e.Value!.ToString(), out DateTime selectedDate) ||
                selectedDate < SqlDateTime.MinValue.Value || selectedDate > SqlDateTime.MaxValue.Value)
            {
                CheckExpiryDate = "Invalid date. Please select a date within the valid range.";
                IsButtonDisabled = true;
            }
            else if (selectedDate < productionBatchModel!.ManufactureDate)
            {
                CheckExpiryDate = "Expiry date cannot be less than manufacture date.";
                IsButtonDisabled = true;
            }
            else
            {
                productionBatchModel!.ExpiryDate = selectedDate;
                CheckExpiryDate = "";
                IsButtonDisabled = false;
            }
        }
        #endregion
        private void ClearData()
        {
            productionBatchModel = new();
        }
    }
}
