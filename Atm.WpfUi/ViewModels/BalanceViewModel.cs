using System.Globalization;
using System.Windows.Input;
using Atm.AtmModel.Implementation;
using Atm.WpfUi.Commands;
using Atm.WpfUi.Services;

namespace Atm.WpfUi.ViewModels;

public class BalanceViewModel : ViewModelBase
{
    public BalanceViewModel(AtmController atm, NavigationService navigateToOperations)
    {
        BackCommand = new BackToOperationsMenuFromBalanceView(navigateToOperations);
        Balance = atm.CheckBalance().ToString(CultureInfo.InvariantCulture);
    }

    public string Balance { get; set; }

    public ICommand BackCommand { get; set; }
}