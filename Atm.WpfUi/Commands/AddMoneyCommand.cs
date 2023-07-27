using SecondTestTask.Models.Atm;
using SecondTestTask.Services;
using SecondTestTask.ViewModels;

namespace SecondTestTask.Commands;

public class AddMoneyCommand : CommandBase
{
    private AtmController _atm;
    private NavigationService _toInvalidOperation;
    private NavigationService _toOperationsMenu;
    private DepositViewModel _vm;

    public AddMoneyCommand(DepositViewModel vm, AtmController atm, NavigationService navigateToOperationsMenu,
        NavigationService toInvalidOperation)
    {
        _vm = vm;
        _atm = atm;
        _toInvalidOperation = toInvalidOperation;
        _toOperationsMenu = navigateToOperationsMenu;
    }

    public override void Execute(object? parameter)
    {
        var cashService = _atm.GetCashTransactionService();
        try
        {
            if (!cashService.TryDeposit(decimal.Parse(_vm.AmountOfMoney)))
            {
                _toInvalidOperation.Navigate();
                _atm.ExtractCard();
                return;
            }

            _toOperationsMenu.Navigate();
        }
        catch
        {
            _toInvalidOperation.Navigate();
            _atm.ExtractCard();
        }
    }
}