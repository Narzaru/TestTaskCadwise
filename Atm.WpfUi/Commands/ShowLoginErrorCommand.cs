using Atm.WpfUi.Services;

namespace Atm.WpfUi.Commands;

public class ShowLoginErrorCommand : CommandBase
{
    private NavigationService _navigationService;

    private ShowLoginErrorCommand(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _navigationService.Navigate();
    }
}