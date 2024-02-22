using Blazored.LocalStorage;
using Client.Services;
using Microsoft.AspNetCore.Components;
using Models.ResponseModel;
using System.Globalization;

namespace Client.Pages.WarehouseManagers
{
    public partial class DetailWarehouses
    {
        #region List Data
        private List<WarehouseResponse> warehouseResponses { get; set; } = default!;
        #endregion
        #region Inject
        [Inject]
        private DetailWarehouseServices detailWarehouseServices { get; set; } = default!;
        [Inject]
        private NavigationManager navigationManager { get; set; } = default!;
        [Inject]
        private ISyncLocalStorageService localStorage { get; set; } = default!;
        #endregion
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        #region Load Data
        protected async Task LoadData()
        {
            var warehouseID = localStorage.GetItem<int>("warehouseID");
            warehouseResponses = await detailWarehouseServices.GetWarehouseInformation(warehouseID);
            foreach (var response in warehouseResponses)
            {
                response.FormattedCostPrice = Int32.Parse(response.CostPrice.ToString()!).ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " VND";
            }
        }
        #endregion
        private void GotoInInventoryPage()
        {
            navigationManager.NavigateTo($"StockInbound");
        }
        private void GotoOutInventoryPage()
        {
            navigationManager.NavigateTo($"StockOutbound");
        }
        private void BackToPage()
        {
            navigationManager.NavigateTo($"warehouse");
        }
    }
}
