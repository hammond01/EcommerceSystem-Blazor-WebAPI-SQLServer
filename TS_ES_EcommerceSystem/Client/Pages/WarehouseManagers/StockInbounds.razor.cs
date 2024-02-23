using Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Models.ResponseModel;
using Models.WarehouseModel;
using Blazored.LocalStorage;
using Radzen;
using System.Globalization;

namespace Client.Pages.WarehouseManagers
{
    public partial class StockInbounds
    {
        #region Variable
        private int pageSize = 10;
        private bool openEditForm = false;
        private bool openDetailDialog = false;
        private string formattedEstimatedPrice = "";
        private string productNameChoose = "";
        private int estimatedPrice = 0;
        private string priceOfBatchChoose { get; set; } = default!;
        private int priceOfBatchChooseNumber;
        [Parameter]
        public int id { get; set; }
        #endregion
        #region ListData
        private List<ProductBathResponse> productionBatches { get; set; } = default!;
        private List<InformationStockInboundFromWarehouse> informationStockInbounds { get; set; } = default!;
        #endregion

        #region Inject
        [Inject]
        NotificationService NotificationService { get; set; } = default!;
        [Inject]
        protected StockInBoundServices stockInBoundServices { get; set; } = default!;
        [Inject]
        protected ProductionBatchServices productionBatchServices { get; set; } = default!;
        [Inject]
        protected WarehouseServices warehouseServices { get; set; } = default!;
        [Inject]
        protected DetailWarehouseServices detailWarehouseServices { get; set; } = default!;
        [Inject]
        protected SweetAlertService Swal { get; set; } = default!;
        [Inject]
        private ISyncLocalStorageService localStorage { get; set; } = default!;
        [Inject]
        private NavigationManager navigationManager { get; set; } = default!;
        #endregion

        [SupplyParameterFromForm]
        StockInBoundResponse? stockInboundModel { get; set; }
        [SupplyParameterFromForm]
        ProductBathResponse? productionBatchModel { get; set; }
        protected override async Task OnInitializedAsync()
        {
            stockInboundModel ??= new();
            productionBatchModel ??= new();
            stockInboundModel.DateInbound = DateTime.Now;
            await LoadData();
        }

        #region Create, Edit and Delete
        protected async Task Create()
        {

            if (stockInboundModel!.InboundID == 0)
            {
                var warehouseID = localStorage.GetItem<int>("warehouseID");
                var batchID = stockInboundModel.ProductionBatchID;
                var moneyOfBatch = (productionBatches.Where(x => x.ProductionBatchID == batchID).FirstOrDefault())!.PriceOfBatch;
                var moneyTotal = moneyOfBatch * stockInboundModel.QuantityInbound;
                var data = new StockInbound
                {
                    DateInbound = DateTime.Now,
                    ProductionBatchID = stockInboundModel.ProductionBatchID,
                    QuantityInbound = stockInboundModel.QuantityInbound,
                    Note = stockInboundModel.Note,
                    TotalPrice = estimatedPrice,
                    WareHouseID = warehouseID
                };
                var res = await stockInBoundServices.CreateStockInBound(data);
                var detailWarehouse = new DetailWarehouseResponse
                {
                    WarehouseID = warehouseID,
                    ActualWarehouse = stockInboundModel.QuantityInbound,
                    CostPrice = moneyTotal,
                    ProductionBatchID = stockInboundModel.ProductionBatchID,
                    Note = ""
                };
                await detailWarehouseServices.CreateDataInWarehouseWhenInbound(detailWarehouse);

                if (res == "Created")
                {
                    await LoadData();
                    ShowNotification(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Summary = "Create",
                        Detail = "Create stock in inventory success!",
                        Duration = 2000
                    });
                    stockInboundModel = new StockInBoundResponse();
                }
                else
                {
                    ShowNotification(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Error,
                        Summary = "Create",
                        Detail = "Create stock in inventory Error!",
                        Duration = 3000
                    });
                }
            }
            else
            {
                var update = await stockInBoundServices.UpdateStockInbound(stockInboundModel);
                if (update == true)
                {
                    ShowNotification(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Summary = "Update",
                        Detail = "Update stock in inventory success!",
                        Duration = 2000
                    });
                    await LoadData();
                    ClearData();
                }
                else
                {
                    ShowNotification(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Error,
                        Summary = "Update",
                        Detail = "Update stock in inventory Error!",
                        Duration = 3000
                    });
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
                    await Swal.FireAsync("Deleted", "Product has been deleted.", SweetAlertIcon.Success);
                    await LoadData();
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
        protected async Task LoadData()
        {
            var warehouseID = localStorage.GetItem<int>("warehouseID");
            productionBatches = await productionBatchServices.GetProductionBatchs();
            informationStockInbounds = await stockInBoundServices.GetInformationFromWarehouseID(warehouseID);
            foreach (var response in informationStockInbounds)
            {
                response.FormattedCostPrice = Int32.Parse(response.TotalPrice.ToString()!).ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " VND";
            }
        }
        private void GetProducts()
        {
            openEditForm = true;
            pageSize = 5;
        }
        protected async Task GetStockInbound(int id)
        {
            stockInboundModel = await stockInBoundServices.GetStockInbound(id);
            openEditForm = true;
            pageSize = 5;
        }
        protected async Task DetaiInbound(int id)
        {
            stockInboundModel = await stockInBoundServices.GetStockInbound(id);
            openDetailDialog = true;
        }
        protected async Task OnChangeProductBath(object value)
        {
            if (value.ToString() != "0")
            {
                var res = await productionBatchServices.GetProductionBatchs();
                var info = res.Where(s => s.ProductionBatchID.ToString() == value.ToString()).FirstOrDefault();
                productNameChoose = info!.Products!.ProductName;
                priceOfBatchChooseNumber = Int32.Parse(info.PriceOfBatch.ToString()!);
                priceOfBatchChoose = priceOfBatchChooseNumber.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
            }
        }
        private void CalculateEstimatedPrice(ChangeEventArgs e)
        {
            var value = 0;
            if (string.IsNullOrEmpty(e.Value!.ToString()))
            {
                value = 0;
            }
            else
            {
                value = Int32.Parse(e.Value.ToString()!);
            }
            estimatedPrice = value * priceOfBatchChooseNumber;
            formattedEstimatedPrice = estimatedPrice.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
        }
        #endregion

        #region Validate
        #endregion
        private void ClearData()
        {
            stockInboundModel = new();
            openEditForm = false;
            pageSize = 10;
        }
        void ShowNotification(NotificationMessage message)
        {
            NotificationService.Notify(message);
        }
        private void BackToPage()
        {
            navigationManager.NavigateTo($"detailWarehouse");
        }
    }
}