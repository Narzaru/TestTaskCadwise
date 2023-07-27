using SecondTestTask.Models.Atm;
using SecondTestTask.Services;
using SecondTestTask.ViewModels;

namespace SecondTestTask.Commands;

public class BackToLoginViewCommand : CommandBase
{
    private AtmController m_atm;
    private NavigationService m_navigationService;

    private OperationsViewModel m_vm;

    public BackToLoginViewCommand(OperationsViewModel vm, AtmController atm, NavigationService navigationService)
    {
        m_vm = vm;
        m_atm = atm;
        m_navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        m_atm.ExtractCard();
        m_navigationService.Navigate();
    }
}