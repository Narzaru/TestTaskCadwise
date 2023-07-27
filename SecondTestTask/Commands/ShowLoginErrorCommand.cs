using SecondTestTask.Services;

namespace SecondTestTask.Commands;

public class ShowLoginErrorCommand : CommandBase
{
    private NavigationService m_navigationService;

    private ShowLoginErrorCommand(NavigationService navigationService)
    {
        m_navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        m_navigationService.Navigate();
    }
}