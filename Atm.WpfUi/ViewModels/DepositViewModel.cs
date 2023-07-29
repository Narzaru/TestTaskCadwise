using System.Windows.Input;
using Atm.AtmModel.Implementation;
using Atm.WpfUi.Commands;
using SecondTestTask.Commands;
using SecondTestTask.Services;
using SecondTestTask.ViewModels;

namespace Atm.WpfUi.ViewModels;

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