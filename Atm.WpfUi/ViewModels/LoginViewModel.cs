using System.Windows.Input;
using Atm.AtmModel.Implementation;
using Atm.WpfUi.Commands;
using Atm.WpfUi.Services;

namespace Atm.WpfUi.ViewModels;

public class LoginViewModel : ViewModelBase
{
    public LoginViewModel(AtmController atm, NavigationService navigateToOperation, NavigationService navigateToError)
    {
        NavigateToOperationsMenu = new LoginViewValidateCardCommand(this, atm, navigateToOperation, navigateToError);
    }

    public string CardId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public ICommand NavigateToOperationsMenu { get; set; }
}