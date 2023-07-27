using SecondTestTask.Services;

namespace SecondTestTask.Commands;

public class BackToOperationsMenuFromBalanceView : CommandBase
{
    private NavigationService _navigationService;

    public BackToOperationsMenuFromBalanceView(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }
}