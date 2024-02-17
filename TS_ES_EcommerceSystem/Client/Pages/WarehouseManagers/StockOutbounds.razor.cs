using Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Models.ResponseModel;
using Models.WarehouseModel;

namespace Client.Pages.WarehouseManagers
{
    public partial class StockOutbounds
    {
        #region Variable
        private bool addButton = false;
        #endregion
        #region ListData
        private List<ResProductionBatch> productionBatches { get; set; } = default!;
        private List<StockOutBoundResponse> stockOutbounds { get; set; } = default!;
        #endregion

        #region Inject
        [Inject]
        protected StockOutBoundServices stockOutBoundServices { get; set; } = default!;
        [Inject]
        protected StockOutBoundServices stockOutBoundServicesa { get; set; } = default!;
        [Inject]
        protected ProductionBatchServices productionBatchServices { get; set; } = default!;
        [Inject]
        protected SweetAlertService Swal { get; set; } = default!;
        #endregion

        [SupplyParameterFromForm]
        StockOutBoundResponse? stockOutboundModel { get; set; }
        protected override async Task OnInitializedAsync()
        {
            stockOutboundModel ??= new();
            stockOutboundModel.DateOutbound = DateTime.Now;
            await LoadData();
        }

        #region Create, Edit and Delete
        protected async Task Create()
        {

            if (stockOutboundModel!.OutboundID == 0)
            {
                //create new product
                var data = new StockOutbound
                {
                    DateOutbound = DateTime.Now,
                    ProductionBatchID = stockOutboundModel.ProductionBatchID,
                    QuantityOutbound = stockOutboundModel.QuantityOutbound,
                    Note = stockOutboundModel.Note

                };
                var res = await stockOutBoundServices.CreateStockOutBound(data);

                if (res == "Created")
                {
                    await Swal.FireAsync(
                     "Create",
                     "Created!",
                     SweetAlertIcon.Success
                     );
                    await LoadData();

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
                var update = await stockOutBoundServices.UpdateStockOutbound(stockOutboundModel);
                if (update == true)
                {
                    await Swal.FireAsync(
                                            "Updated",
                                            "Updated.",
                                             SweetAlertIcon.Success
                                        );
                    await LoadData();
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
        protected async Task Delete(int id)
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
                var res = await stockOutBoundServices.DeleteStockOutbound(id);


                if (res == "Deleted")
                {
                    await Swal.FireAsync(
                                             "Deleted",
                                             "Product has been deleted.",
                                             SweetAlertIcon.Success
                                        );
                    await LoadData();
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
        protected async Task LoadData()
        {
            productionBatches = await productionBatchServices.GetProductionBatchs();
            stockOutbounds = await stockOutBoundServices.GetStockOutbounds();
        }
        private void GetProducts()
        {
            addButton = true;
        }
        protected async Task GetStockOutbound(int id)
        {
            stockOutboundModel = await stockOutBoundServices.GetStockOutbound(id);
        }
        #endregion

        #region Validate
        #endregion
        private void ClearData()
        {
            stockOutboundModel = new();
        }
    }
}