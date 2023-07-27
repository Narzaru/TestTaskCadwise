using SecondTestTask.Services;

namespace SecondTestTask.Commands;

public class BackToOperationsMenuFromBalanceView : CommandBase
{
    private NavigationService m_navigationService;

    public BackToOperationsMenuFromBalanceView(NavigationService navigationService)
    {
        m_navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        m_navigationService.Navigate();
    }
}