using System.Windows.Input;
using SecondTestTask.Commands;
using SecondTestTask.Models.Atm;
using SecondTestTask.Services;

namespace SecondTestTask.ViewModels;

public class LoginViewModel : ViewModelBase
{
    public LoginViewModel(AtmController atm, NavigationService navigateToOperation, NavigationService navigateToError)
    {
        NavigateToOperationsMenu = new ValidateCardCommand(this, atm, navigateToOperation, navigateToError);
    }

    public string CardId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public ICommand NavigateToOperationsMenu { get; set; }
}