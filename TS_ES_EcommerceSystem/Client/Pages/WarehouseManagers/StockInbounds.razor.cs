using Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Models.ResponseModel;
using Models.WarehouseModel;
using Radzen.Blazor;
using Radzen;

namespace Client.Pages.WarehouseManagers
{
    public partial class StockInbounds
    {
        #region Variable
        private bool addButton = false;
        #endregion
        #region ListData
        private List<ResProductionBatch> productionBatches { get; set; } = default!;
        private List<StockInBoundResponse> stockInbounds { get; set; } = default!;
        #endregion

        #region Inject
        [Inject]
        protected StockInBoundServices stockInBoundServices { get; set; } = default!;
        [Inject]
        protected ProductionBatchServices productionBatchServices { get; set; } = default!;
        [Inject]
        protected SweetAlertService Swal { get; set; } = default!;
        #endregion

        [SupplyParameterFromForm]
        StockInBoundResponse? stockInboundModel { get; set; }
        protected override async Task OnInitializedAsync()
        {
            stockInboundModel ??= new();
            stockInboundModel.DateInbound = DateTime.Now;
            await LoadData();
        }

        #region Create, Edit and Delete
        protected async Task Create()
        {

            if (stockInboundModel!.InboundID == 0)
            {
                var data = new StockInbound
                {
                    DateInbound = DateTime.Now,
                    ProductionBatchID = stockInboundModel.ProductionBatchID,
                    QuantityInbound = stockInboundModel.QuantityInbound,
                    Note = stockInboundModel.Note

                };
                var res = await stockInBoundServices.CreateStockInBound(data);

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
                var update = await stockInBoundServices.UpdateStockInbound(stockInboundModel);
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
                var res = await stockInBoundServices.DeleteStockInbound(id);


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
            stockInbounds = await stockInBoundServices.GetStockInbounds();
        }
        private void GetProducts()
        {
            addButton = true;
        }
        protected async Task GetStockInbound(int id)
        {
            stockInboundModel = await stockInBoundServices.GetStockInbound(id);
        }
        #endregion

        #region Validate
        #endregion
        private void ClearData()
        {
            stockInboundModel = new();
        }
    }
}