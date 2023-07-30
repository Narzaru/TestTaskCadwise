using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Atm.AtmModel.Implementation;
using Atm.AtmModel.Services;
using Atm.WpfUi.Commands;
using Atm.WpfUi.Services;

namespace Atm.WpfUi.ViewModels;

public class DenominationSelectionViewModel : ViewModelBase
{
    private AtmController _atm;

    public DenominationSelectionViewModel(
        AtmController atm,
        NavigationService navigateToOperationsMenu,
        NavigationService navigateToWithdrawMenu,
        NavigationService navigateToOperationError)
    {
        _atm = atm;
        BackToWithdrawView = new NavigateCommand(navigateToWithdrawMenu);
        WithdrawMoney =
            new DenominationSelectionWithdrawCommand(this, atm, navigateToOperationsMenu, navigateToOperationError);
        WithdrawnMoneys = new ObservableCollection<MoneyStack>();
        DenominationsComboBox = new ObservableCollection<decimal>();
        foreach (var denomination in atm.GetTraysDenomination())
        {
            DenominationsComboBox.Add(denomination);
        }

        SelectedDenomination = DenominationsComboBox.FirstOrDefault();
    }

    public ObservableCollection<decimal> DenominationsComboBox { get; set; }
    public ObservableCollection<MoneyStack> WithdrawnMoneys { get; set; }
    public decimal SelectedDenomination { get; set; }
    public ICommand BackToWithdrawView { get; set; }
    public ICommand WithdrawMoney { get; set; }
}