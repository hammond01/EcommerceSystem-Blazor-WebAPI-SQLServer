using Blazored.LocalStorage;
using Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Models.WarehouseModel;
using Radzen;

namespace Client.Pages.WarehouseManagers
{
    public partial class Warehouses
    {
        private bool openEditForm = false;
        private int pageSize = 5;
        private List<WareHouse>? warehouses { get; set; } = default!;
        [Inject]
        protected WarehouseServices warehouseServices { get; set; } = default!;
        [Inject]
        private NavigationManager navigationManager { get; set; } = default!;
        [Inject]
        private ISyncLocalStorageService localStorage { get; set; } = default!;
        [Inject]
        NotificationService NotificationService { get; set; } = default!;
        [Inject]
        protected SweetAlertService Swal { get; set; } = default!;
        [SupplyParameterFromForm]
        WareHouse? warehouseModel { get; set; }
        protected override async Task OnInitializedAsync()
        {
            warehouseModel ??= new();
            await LoadData();
        }
        protected async Task LoadData()
        {

            warehouses = await warehouseServices.GetWareHouses();
        }
        private void Detail(int id)
        {
            localStorage.SetItem("warehouseID", id);
            //Using parameter
            //navigationManager.NavigateTo($"detailWarehouse/{id}");
            navigationManager.NavigateTo($"detailWarehouse");
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
                var data = await warehouseServices.Delete(id);
                if (data)
                {
                    await Swal.FireAsync("Deleted", "Deleted.", SweetAlertIcon.Success);
                    await LoadData();
                }
                else
                {
                    await Swal.FireAsync("Deleted", "Inventory hasn't been deleted.", SweetAlertIcon.Error);
                }
            }
            else if (result.Dismiss == DismissReason.Cancel)
            {
                await Swal.FireAsync("Cancelled", "Inventory is safe :)", SweetAlertIcon.Error);
            }
        }
        protected async Task Create()
        {
            if (warehouseModel!.WareHouseID == 0)
            {
                var data = new WareHouse
                {
                    WarehouseName = warehouseModel.WarehouseName,
                    Address = warehouseModel.Address,
                    Note = warehouseModel.Note

                };
                var res = await warehouseServices.Create(data);

                if (res)
                {
                    await Swal.FireAsync("Create", "Created!", SweetAlertIcon.Success);
                    warehouseModel = new();
                    await LoadData();
                }
                else
                {
                    await Swal.FireAsync("Cancelled", "Create is safe :)", SweetAlertIcon.Error);
                }
            }
            else
            {
                var update = await warehouseServices.Update(warehouseModel);
                if (update == true)
                {
                    await Swal.FireAsync("Updated", "Updated.", SweetAlertIcon.Success);
                    await LoadData();
                }
                else
                {
                    await Swal.FireAsync("Error", "Error", SweetAlertIcon.Error);
                }
            }
        }
        private void GetForm()
        {
            openEditForm = true;
            pageSize = 5;
        }
        private void Close()
        {
            openEditForm = false;
            warehouseModel = new();
            pageSize = 10;
        }
        void ShowNotification(NotificationMessage message)
        {
            NotificationService.Notify(message);
        }
    }
}
