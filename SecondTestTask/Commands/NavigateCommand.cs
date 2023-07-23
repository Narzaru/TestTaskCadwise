using SecondTestTask.Services;

namespace SecondTestTask.Commands;

public class NavigateCommand : CommandBase
{
    private readonly NavigationService m_navigationService;

    public NavigateCommand(NavigationService navigationService)
    {
        m_navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        m_navigationService.Navigate();
    }
}