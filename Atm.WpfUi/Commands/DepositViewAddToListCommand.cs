using System.Collections.Generic;
using System.Linq;
using Atm.WpfUi.ViewModels;
using SecondTestTask.Commands;

namespace Atm.WpfUi.Commands;

public class DepositViewAddToListCommand : CommandBase
{
    public DepositViewAddToListCommand(DepositViewModel vm)
    {
        _vm = vm;
    }

    public override void Execute(object? parameter)
    {
        if (_vm.QuantityOfDenomination == 0) return;
        var denomination = _vm.SelectedDenomination;
        var quantity = _vm.QuantityOfDenomination;

        if (_vm.InsertedMoneys.FirstOrDefault(row => row.Key == denomination)
            .Equals(default(KeyValuePair<decimal, int>)))
        {
            _vm.InsertedMoneys.Add(new(denomination, quantity));
        }
        else
        {
            for (var i = 0; i < _vm.InsertedMoneys.Count; ++i)
            {
                if (_vm.InsertedMoneys[i].Key == denomination)
                {
                    _vm.InsertedMoneys[i] =
                        new KeyValuePair<decimal, int>(denomination, _vm.InsertedMoneys[i].Value + quantity);
                }
            }
        }
    }

    private DepositViewModel _vm;
}