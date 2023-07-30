using Atm.WpfUi.Services;

namespace Atm.WpfUi.Commands;

public class AfterInvalidOkCommand : CommandBase
{
    private NavigationService _navigationService;

    public AfterInvalidOkCommand(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }
}