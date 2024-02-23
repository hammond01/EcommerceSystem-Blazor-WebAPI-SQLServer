using Models.RequestModel;

namespace Client.Pages.Authentication
{
    public partial class Register
    {
        private RegisterRequest RegisterModel = new RegisterRequest();
        private bool ShowErrors;
        private IEnumerable<string>? Errors;

        private void HandleRegistration()
        {
            ShowErrors = false;
            // var result = await AuthService.Register(RegisterModel);

            // if (result.Successful)
            // {
            //     NavigationManager.NavigateTo("/login");
            // }
            // else
            // {
            //     Errors = result.Errors;
            //     ShowErrors = true;
            // }
        }
    }
}