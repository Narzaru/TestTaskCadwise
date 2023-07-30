using System.Collections.Generic;
using System.Linq;
using Atm.AtmModel.Implementation;
using Atm.AtmModel.Services;
using Atm.WpfUi.ViewModels;
using SecondTestTask.Commands;
using SecondTestTask.Services;

namespace Atm.WpfUi.Commands;

public class DepositViewAddMoneyCommand : CommandBase
{
    private AtmController _atm;
    private NavigationService _toInvalidOperation;
    private NavigationService _toOperationsMenu;
    private DepositViewModel _vm;

    public DepositViewAddMoneyCommand(
        DepositViewModel vm,
        AtmController atm,
        NavigationService navigateToOperationsMenu,
        NavigationService toInvalidOperation)
    {
        _vm = vm;
        _atm = atm;
        _toInvalidOperation = toInvalidOperation;
        _toOperationsMenu = navigateToOperationsMenu;
    }

    public override void Execute(object? parameter)
    {
        var moneyStack = _vm.InsertedMoneys.Select(im => new MoneyStack { Denomination = im.Key, Quantity = im.Value });
        var notAccepted = _atm.InsertMoneyStacks(moneyStack).ToList();
        _vm.InsertedMoneys.Clear();

        if (notAccepted.Any())
        {
            var moneys = notAccepted.Select(ms => new KeyValuePair<decimal, int>(ms.Denomination, ms.Quantity));
            foreach (var money in moneys)
            {
                _vm.InsertedMoneys.Add(money);
            }
        }
    }
}