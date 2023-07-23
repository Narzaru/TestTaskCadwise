using SecondTestTask.Services;
using SecondTestTask.ViewModels;

namespace SecondTestTask.Commands;

public class ShowLoginErrorCommand : CommandBase
{
    ShowLoginErrorCommand(NavigationService navigationService)
    {
        m_navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        m_navigationService.Navigate();
    }

    private NavigationService m_navigationService;
}