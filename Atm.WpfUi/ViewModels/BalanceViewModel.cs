using System.Globalization;
using System.Windows.Input;
using Atm.AtmModel.Implementation;
using SecondTestTask.Commands;
using SecondTestTask.Services;
using SecondTestTask.ViewModels;

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