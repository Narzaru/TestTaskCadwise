using System;
using System.Collections.Generic;
using Atm.AtmModel.Implementation;
using Atm.AtmModel.Services;
using Atm.WpfUi.Services;
using Atm.WpfUi.ViewModels;

namespace Atm.WpfUi.Commands;

public class DenominationSelectionWithdrawCommand : CommandBase
{
    private AtmController _atm;
    private NavigationService _toInvalidOperation;
    private NavigationService _toOperationsMenu;
    private DenominationSelectionViewModel _vm;

    public DenominationSelectionWithdrawCommand(
        DenominationSelectionViewModel vm,
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
        var denomination = _vm.SelectedDenomination;
        var amount = _toOperationsMenu.MessageBus.Get() is decimal
            ? (decimal)_toOperationsMenu.MessageBus.Get()
            : throw new InvalidCastException();

        var result = _atm.PullMoneyStacks(amount, denomination);
        try
        {
            result.Value.Accept();
        }
        catch
        {
            _atm.ExtractCard();
            _toInvalidOperation.Navigate();
            return;
        }

        _vm.WithdrawnMoneys.Clear();
        foreach (var moneyStack in result.Key)
        {
            _vm.WithdrawnMoneys.Add(moneyStack);
        }
    }
}