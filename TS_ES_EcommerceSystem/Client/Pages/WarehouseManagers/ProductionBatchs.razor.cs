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
        private string ErrorMessage { get; set; } = default!;
        private bool IsButtonDisabled = true;
        private bool addProductionBatch = false;
        private string CheckManufactureDate { get; set; } = default!;
        private string CheckExpiryDate { get; set; } = default!;
        #endregion

        #region ListData
        private List<ResProductionBatch> productionBatches { get; set; } = default!;
        private List<Products>? products { get; set; } = default!;
        #endregion

        #region Inject
        [Inject]
        protected ProductServices productServices { get; set; } = default!;
        [Inject]
        protected ProductionBatchServices productionBatchServices { get; set; } = default!;
        [Inject]
        protected SweetAlertService Swal { get; set; } = default!;
        #endregion

        [SupplyParameterFromForm]
        ResProductionBatch? productionBatchModel { get; set; }
        protected override async Task OnInitializedAsync()
        {
            productionBatchModel ??= new ResProductionBatch();
            productionBatchModel.ExpiryDate = DateTime.Now;
            productionBatchModel.ManufactureDate = DateTime.Now;
            await GetProductBatchs();
        }

        #region Create, Edit and Delete
        protected async Task CreateProductionBatch()
        {

            if (productionBatchModel!.ProductionBatchID == 0)
            {
                //create new product
                var data = new ProductionBatch
                {
                    ProductionBatchName = productionBatchModel.ProductionBatchName,
                    ProductID = productionBatchModel.ProductID,
                    Quantity = productionBatchModel.Quantity,
                    ManufactureDate = productionBatchModel.ManufactureDate,
                    ExpiryDate = productionBatchModel.ExpiryDate,
                };
                var res = await productionBatchServices.CreateProductionBatch(data);

                if (res == "Created")
                {
                    await Swal.FireAsync(
                     "Create",
                     "Created!",
                     SweetAlertIcon.Success
                     );
                    await GetProductBatchs();

                }
                else
                {
                    await Swal.FireAsync(
                     "Cancelled",
                     "Create is safe :)",
                     SweetAlertIcon.Error
                     );
                }
            }
            else
            {
                // Update product
                var update = await productionBatchServices.UpdateProductionBatch(productionBatchModel);
                if (update == true)
                {
                    await Swal.FireAsync(
                                            "Updated",
                                            "Updated.",
                                             SweetAlertIcon.Success
                                        );
                    await GetProductBatchs();
                }
                else
                {
                    await Swal.FireAsync(
                                            "Error",
                                            "Error",
                                            SweetAlertIcon.Error
                                        );
                }
            }

        }
        protected async Task DeleteProductbatch(int id)
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
                var res = await productionBatchServices.DeleteProductionBatch(id);


                if (res == "Deleted")
                {
                    await Swal.FireAsync(
                                             "Deleted",
                                             "Product has been deleted.",
                                             SweetAlertIcon.Success
                                        );
                    await GetProductBatchs();
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
        }
        private void GetProducts()
        {
            addProductionBatch = true;
        }
        #endregion

        #region Validate
        private void ValidateInput(ChangeEventArgs e)
        {
            string input = e.Value!.ToString()!;
            if (input.Length != 4)
            {
                IsButtonDisabled = true;
                ErrorMessage = "Product batch name must be 4 characters long.";
            }
            else
            {
                IsButtonDisabled = false;
                ErrorMessage = "";
            }
        }
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
    }
}
