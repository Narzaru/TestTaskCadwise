using Atm.AtmModel.Implementation;
using Atm.WpfUi.ViewModels;
using SecondTestTask.Commands;
using SecondTestTask.Services;
using SecondTestTask.ViewModels;

namespace Atm.WpfUi.Commands;

public class GetMoneyCommand : CommandBase
{
    private AtmController _atm;
    private NavigationService _toInvalidOperation;
    private NavigationService _toOperationsMenu;
    private WithdrawViewModel _vm;

    public GetMoneyCommand(WithdrawViewModel vm, AtmController atm, NavigationService navigateToOperationsMenu,
        NavigationService toInvalidOperation)
    {
        _vm = vm;
        _atm = atm;
        _toInvalidOperation = toInvalidOperation;
        _toOperationsMenu = navigateToOperationsMenu;
    }

    public override void Execute(object? parameter)
    {
        // var cashService = _atm.GetCashTransactionService();
        // try
        // {
        //     if (!cashService.TryWithdraw(decimal.Parse(_vm.AmountOfMoney)))
        //     {
        //         _toInvalidOperation.Navigate();
        //         _atm.ExtractCard();
        //         return;
        //     }
        //
        //     _toOperationsMenu.Navigate();
        // }
        // catch
        // {
        //     _toInvalidOperation.Navigate();
        //     _atm.ExtractCard();
        // }
    }
}