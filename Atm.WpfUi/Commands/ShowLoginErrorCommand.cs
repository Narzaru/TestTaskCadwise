using SecondTestTask.Services;

namespace SecondTestTask.Commands;

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