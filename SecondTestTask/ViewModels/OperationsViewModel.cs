using System.Windows.Input;
using SecondTestTask.Commands;
using SecondTestTask.Models.Atm;
using SecondTestTask.Navigation;
using SecondTestTask.Services;

namespace SecondTestTask.ViewModels;

public class OperationsViewModel : ViewModelBase
{
    public OperationsViewModel(AtmController atm,
        NavigationService navigateToLogin,
        NavigationService navigateToBalance,
        NavigationService navigateToDeposit,
        NavigationService navigateToWithdraw)
    {
        NavigateToLogInMenu = new BackToLoginViewCommand(this, atm, navigateToLogin);
        CheckBalanceCommand = new NavigateCommand(navigateToBalance);
        NavigateToDeposit = new NavigateCommand(navigateToDeposit);
        WithdrawCommand = new NavigateCommand(navigateToWithdraw);
    }

    public ICommand NavigateToLogInMenu { get; set; }
    public ICommand CheckBalanceCommand { get; set; }
    public ICommand NavigateToDeposit { get; set; }
    public ICommand WithdrawCommand { get; set; }
}