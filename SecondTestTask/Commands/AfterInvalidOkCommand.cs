using SecondTestTask.Services;

namespace SecondTestTask.Commands;

public class AfterInvalidOkCommand : CommandBase
{
    private NavigationService m_navigationService;

    public AfterInvalidOkCommand(NavigationService navigationService)
    {
        m_navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        m_navigationService.Navigate();
    }
}