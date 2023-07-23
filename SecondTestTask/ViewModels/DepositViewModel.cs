using System.Windows.Input;
using SecondTestTask.Commands;
using SecondTestTask.Models.Atm;
using SecondTestTask.Services;

namespace SecondTestTask.ViewModels;

public class DepositViewModel : ViewModelBase
{
    public DepositViewModel(AtmController atm, NavigationService toOperationsMenu,
        NavigationService toInvalidOperation)
    {
        BackToOperationsCommand = new NavigateCommand(toOperationsMenu);
        ExecuteOperationCommand = new AddMoneyCommand(this, atm, toOperationsMenu, toInvalidOperation);
    }

    public string AmountOfMoney { get; set; } = string.Empty;

    public ICommand BackToOperationsCommand { get; set; }
    public ICommand ExecuteOperationCommand { get; set; }
}