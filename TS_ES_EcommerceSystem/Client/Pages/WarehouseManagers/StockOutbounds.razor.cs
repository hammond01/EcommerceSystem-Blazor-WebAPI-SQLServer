using Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Models.ResponseModel;
using Models.WarehouseModel;
using Blazored.LocalStorage;
using Radzen;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace Client.Pages.WarehouseManagers
{
    public partial class StockOutbounds
    {
        #region Variable
        private int pageSize = 10;
        private bool openEditForm = false;
        private bool openDetailDialog = false;
        private bool buttonSaveOutBound = true;
        private bool showListProductInWarehouse = false;
        private string productNameChoose = "";
        private string checkQuantityOutBound { get; set; } = default!;
        [Parameter]
        public int id { get; set; }
        #endregion
        #region ListData
        private List<ProductBathResponse> productionBatches { get; set; } = default!;
        private List<InformationStockOutboundFromWarehouse> informationStockOutbounds { get; set; } = default!;
        private List<WarehouseResponse> listWarehouseResponses { get; set; } = new List<WarehouseResponse>();
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
        protected StockOutBoundServices stockOutBoundServices { get; set; } = default!;
        [Inject]
        protected SweetAlertService Swal { get; set; } = default!;
        [Inject]
        private ISyncLocalStorageService localStorage { get; set; } = default!;
        [Inject]
        private NavigationManager navigationManager { get; set; } = default!;
        #endregion

        [SupplyParameterFromForm]
        StockOutBoundResponse? stockOutBoundModel { get; set; }
        [SupplyParameterFromForm]
        WarehouseResponse? warehouseResponseModel { get; set; }
        [SupplyParameterFromForm]
        InformationStockOutboundFromWarehouse? informationStockOutboundModel { get; set; }
        [SupplyParameterFromForm]
        ProductBathResponse? productionBatchModel { get; set; }
        protected override async Task OnInitializedAsync()
        {
            informationStockOutboundModel ??= new();
            warehouseResponseModel ??= new();
            stockOutBoundModel ??= new();
            productionBatchModel ??= new();
            await LoadData();
        }

        #region Create, Edit and Delete
        protected async Task Create()
        {
            if (stockOutBoundModel!.OutboundID == 0)
            {
                var warehouseID = localStorage.GetItem<int>("warehouseID");
                var req = new StockOutBoundResponse()
                {
                    DateOutbound = DateTime.Now,
                    ProductionBatchID = warehouseResponseModel!.ProductionBatchID,
                    QuantityOutbound = stockOutBoundModel.QuantityOutbound,
                    WareHouseID = warehouseID,
                    Note = stockOutBoundModel.Note
                };
                var res = await stockOutBoundServices.CreateStockOutBound(req);
                if (res)
                {
                    var demo = req!.ProductionBatchID;
                    var inforProductBatch = await productionBatchServices.GetProductionBatchById(req!.ProductionBatchID);
                    var inforProductBatchInWarehouse = listWarehouseResponses.Where(h => h.ProductionBatchID == req.ProductionBatchID).FirstOrDefault();

                    var updateDetailWarehouseAfterOutWarehouse = new DetailWarehouseResponse()
                    {
                        DetailWarehouseID = warehouseResponseModel.DetailWarehouseID,
                        ActualWarehouse = inforProductBatchInWarehouse!.ActualWarehouse - stockOutBoundModel.QuantityOutbound,
                        CostPrice = inforProductBatchInWarehouse.CostPrice - (stockOutBoundModel.QuantityOutbound * inforProductBatch.PriceOfBatch),
                        Note = ""
                    };
                    var responseUpdateDetailWarehouseTable = await detailWarehouseServices.Update(updateDetailWarehouseAfterOutWarehouse);
                    if (responseUpdateDetailWarehouseTable)
                    {
                        await LoadData();
                        ShowNotification(new NotificationMessage
                        {
                            Severity = NotificationSeverity.Success,
                            Summary = "Create",
                            Detail = "Create stock in inventory success!",
                            Duration = 2000
                        });
                        await StockOutBound();
                    }
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
                //var update = await stockInBoundServices.UpdateStockInbound(stockInboundModel!);
                //if (update == true)
                //{
                //    ShowNotification(new NotificationMessage
                //    {
                //        Severity = NotificationSeverity.Success,
                //        Summary = "Update",
                //        Detail = "Update stock in inventory success!",
                //        Duration = 2000
                //    });
                //    await LoadData();
                //    ClearData();
                //}
                //else
                //{
                //    ShowNotification(new NotificationMessage
                //    {
                //        Severity = NotificationSeverity.Error,
                //        Summary = "Update",
                //        Detail = "Update stock in inventory Error!",
                //        Duration = 3000
                //    });
                //}
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
            informationStockOutbounds = await stockOutBoundServices.GetInformationOutBoundFromWarehouseID(warehouseID);
        }
        private void GetProducts()
        {
            openEditForm = true;
            pageSize = 5;
        }
        protected async Task GetStockOutbound(int id)
        {
            var warehouseID = localStorage.GetItem<int>("warehouseID");
            stockOutBoundModel = await stockOutBoundServices.GetStockOutbound(id);
            productNameChoose = await GetProductNameChoose(stockOutBoundModel.ProductionBatchID!);
            openEditForm = true;
            pageSize = 5;
        }
        protected async Task DetaiOutbound(int id)
        {
            var warehouseID = localStorage.GetItem<int>("warehouseID");
            informationStockOutboundModel = await stockOutBoundServices.GetInfoOutBoundByOutBoundID(warehouseID, id);
            openDetailDialog = true;
        }
        protected async Task OnChangeProductBath(object value)
        {
            if (value.ToString() != "0")
            {
                productNameChoose = await GetProductNameChoose(value);
            }
        }
        #endregion

        #region Validate
        #endregion
        private void ClearData()
        {
            openEditForm = false;
            stockOutBoundModel = new();
            checkQuantityOutBound = "";
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
        protected async Task<string> GetProductNameChoose(object value)
        {
            var res = await productionBatchServices.GetProductionBatchs();
            var infoById = res.Where(s => s.ProductionBatchID.ToString() == value.ToString()).FirstOrDefault();
            if (infoById != null)
            {
                var valueReturn = infoById!.Products!.ProductName;
                return valueReturn;
            }
            else
            {
                var infoByName = res.Where(s => s.ProductionBatchName!.ToString() == value.ToString()).FirstOrDefault();
                var valueReturn = infoByName!.Products!.ProductName;
                return valueReturn;
            }

        }
        protected async Task StockOutBound()
        {
            showListProductInWarehouse = true;
            var warehouseID = localStorage.GetItem<int>("warehouseID");
            listWarehouseResponses = await stockOutBoundServices.GetInfoWarehouseActualWarehouseGreaterThanZeroByWarehouseID(warehouseID);
            foreach (var response in listWarehouseResponses)
            {
                response.FormattedCostPrice = Int32.Parse(response.CostPrice.ToString()!).ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " VND";
            }
        }
        protected async Task OptionStockOutBound(int id)
        {
            var warehouseID = localStorage.GetItem<int>("warehouseID");
            warehouseResponseModel = await stockOutBoundServices.GetInfoStockOutbound(warehouseID, id);
            productNameChoose = await GetProductNameChoose(warehouseResponseModel.ProductionBatchName!);
        }
        private void ShowListStockInbound()
        {
            showListProductInWarehouse = false;
            listWarehouseResponses = new List<WarehouseResponse>();
        }
        private void CheckQuantity(ChangeEventArgs e)
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
            if (value > warehouseResponseModel!.ActualWarehouse)
            {
                buttonSaveOutBound = true;
                checkQuantityOutBound = "Số lượng lớn hơn số lượng đang có trong kho";
            }
            else
            {
                buttonSaveOutBound = false;
                checkQuantityOutBound = "";
            }
        }
    }
}