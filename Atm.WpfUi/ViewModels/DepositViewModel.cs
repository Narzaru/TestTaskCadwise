using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Atm.AtmModel.Implementation;
using Atm.WpfUi.Commands;
using Atm.WpfUi.Services;

namespace Atm.WpfUi.ViewModels;

public class DepositViewModel : ViewModelBase
{
    public DepositViewModel(
        AtmController atm,
        NavigationService toOperationsMenu,
        NavigationService toInvalidOperation)
    {
        BackToOperationsCommand = new NavigateCommand(toOperationsMenu);
        ExecuteOperationCommand = new DepositViewAddMoneyCommand(this, atm, toOperationsMenu, toInvalidOperation);
        ComboBoxDataDenomination = new ObservableCollection<decimal>();
        AddToListCommand = new DepositViewAddToListCommand(this);
        InsertedMoneys = new ObservableCollection<KeyValuePair<decimal, int>>();
        foreach (var denomination in atm.GetTraysDenomination())
        {
            ComboBoxDataDenomination.Add(denomination);
        }

        SelectedDenomination = ComboBoxDataDenomination.First();
    }

    public ObservableCollection<decimal> ComboBoxDataDenomination { get; set; }

    public decimal SelectedDenomination { get; set; }

    public int QuantityOfDenomination { get; set; }

    public ObservableCollection<KeyValuePair<decimal, int>> InsertedMoneys { get; set; }

    public ICommand BackToOperationsCommand { get; set; }
    public ICommand ExecuteOperationCommand { get; set; }
    public ICommand AddToListCommand { get; set; }
}