using SecondTestTask.Services;

namespace SecondTestTask.Commands;

public class BackToOperationsMenuFromBalanceView : CommandBase
{
    public BackToOperationsMenuFromBalanceView(NavigationService navigationService)
    {
        m_navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        m_navigationService.Navigate();
    }

    private NavigationService m_navigationService;
}