using System.Windows.Input;
using Atm.AtmModel.Implementation;
using Atm.WpfUi.Commands;
using Atm.WpfUi.Services;

namespace Atm.WpfUi.ViewModels;

public class OperationsViewModel : ViewModelBase
{
    public OperationsViewModel(AtmController atm,
        NavigationService navigateToLogin,
        NavigationService navigateToBalance,
        NavigationService navigateToDeposit,
        NavigationService navigateToWithdraw)
    {
        NavigateToLogInMenu = new OperationsViewBackToLoginViewCommand(this, atm, navigateToLogin);
        CheckBalanceCommand = new NavigateCommand(navigateToBalance);
        NavigateToDeposit = new NavigateCommand(navigateToDeposit);
        WithdrawCommand = new NavigateCommand(navigateToWithdraw);
    }

    public ICommand NavigateToLogInMenu { get; set; }
    public ICommand CheckBalanceCommand { get; set; }
    public ICommand NavigateToDeposit { get; set; }
    public ICommand WithdrawCommand { get; set; }
}