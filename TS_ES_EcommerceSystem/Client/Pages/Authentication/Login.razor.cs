using Client.Services;
using Microsoft.AspNetCore.Components;
using Models.RequestModel;

namespace Client.Pages.Authentication
{
    public partial class Login
    {
        private LoginRequest loginModel = new LoginRequest();
        private bool ShowErrors;
        private string Error = "";
        [Inject] NavigationManager NavigationManager { get; set; } = default!;
        [Inject] AuthServices authServices { get; set; } = default!;

        private async Task HandleLogin()
        {
            ShowErrors = false;

            var result = await authServices.Login(loginModel);

            if (result.Status == 200)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Error = result.Message!;
                ShowErrors = true;
            }
        }
    }
}