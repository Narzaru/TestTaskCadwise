using System.Linq;
using Atm.AtmModel.Implementation;
using Atm.WpfUi.Services;
using Atm.WpfUi.ViewModels;

namespace Atm.WpfUi.Commands;

public class WithdrawToDenominationSelectionCommand : CommandBase
{
    private readonly WithdrawViewModel _vm;
    private readonly AtmController _atm;
    private readonly NavigationService _toDenominationSelectionMenu;
    private readonly NavigationService _toOperationErrorMenu;

    public WithdrawToDenominationSelectionCommand(
        WithdrawViewModel vm,
        AtmController atm,
        NavigationService toDenominationSelectionMenu,
        NavigationService toOperationErrorMenu)
    {
        _vm = vm;
        _atm = atm;
        _toDenominationSelectionMenu = toDenominationSelectionMenu;
        _toOperationErrorMenu = toOperationErrorMenu;
    }

    public override void Execute(object? parameter)
    {
        if (_atm.CheckBalance() >= _vm.AmountOfMoney && _vm.AmountOfMoney > 0)
        {
            _toDenominationSelectionMenu.MessageBus.Set(_vm.AmountOfMoney);
            _toDenominationSelectionMenu.Navigate();
        }
        else
        {
            _atm.ExtractCard();
            _toOperationErrorMenu.Navigate();
        }
    }
}