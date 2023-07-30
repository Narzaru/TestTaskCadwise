using Atm.WpfUi.Services;

namespace Atm.WpfUi.Commands;

public class BalanceViewBackToOperationsMenu : CommandBase
{
    private NavigationService _navigationService;

    public BalanceViewBackToOperationsMenu(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }
}