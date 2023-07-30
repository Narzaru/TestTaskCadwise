using Atm.WpfUi.Services;

namespace Atm.WpfUi.Commands;

public class InvalidOperationViewAfterInvalidOkCommand : CommandBase
{
    private NavigationService _navigationService;

    public InvalidOperationViewAfterInvalidOkCommand(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }
}