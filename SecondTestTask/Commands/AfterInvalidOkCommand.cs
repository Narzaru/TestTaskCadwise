using SecondTestTask.Services;

namespace SecondTestTask.Commands;

public class AfterInvalidOkCommand : CommandBase
{
    public AfterInvalidOkCommand(NavigationService navigationService)
    {
        m_navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        m_navigationService.Navigate();
    }

    private NavigationService m_navigationService;
}