using System.Globalization;
using System.Windows.Input;
using SecondTestTask.Commands;
using SecondTestTask.Models.Atm;
using SecondTestTask.Services;

namespace SecondTestTask.ViewModels;

public class BalanceViewModel : ViewModelBase
{
    public BalanceViewModel(AtmController atm, NavigationService navigateToOperations)
    {
        BackCommand = new BackToOperationsMenuFromBalanceView(navigateToOperations);
        Balance = atm.GetCashTransactionService().Balance().ToString(CultureInfo.InvariantCulture);
    }

    public string Balance { get; set; }

    public ICommand BackCommand { get; set; }
}