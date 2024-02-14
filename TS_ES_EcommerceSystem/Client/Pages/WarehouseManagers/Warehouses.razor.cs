using Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Models;
using Models.WarehouseModel;

namespace Client.Pages.WarehouseManagers
{
    public partial class Warehouses
    {
        private List<WareHouse>? warehouses;

        [Inject]
        protected WarehouseServices warehouseServices { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadWarehouse();
        }

        protected async Task LoadWarehouse()
        {
            warehouses = await warehouseServices.GetWareHouses();
        }

    }
}
