using System.Windows.Input;
using Atm.AtmModel.Implementation;
using Atm.WpfUi.Commands;
using Atm.WpfUi.Services;
using Atm.WpfUi.Views;

namespace Atm.WpfUi.ViewModels;

public class WithdrawViewModel : ViewModelBase
{
    public WithdrawViewModel(
        AtmController atm,
        NavigationService toOperationsMenu,
        NavigationService toDenominationSelectionMenu,
        NavigationService toOperationErrorMenu)
    {
        BackToOperationsCommand = new NavigateCommand(toOperationsMenu);
        ContinueCommand = new WithdrawToDenominationSelectionCommand(this, atm, toDenominationSelectionMenu, toOperationErrorMenu);
    }

    public decimal AmountOfMoney { get; set; }

    public ICommand BackToOperationsCommand { get; set; }
    public ICommand ContinueCommand { get; set; }
}