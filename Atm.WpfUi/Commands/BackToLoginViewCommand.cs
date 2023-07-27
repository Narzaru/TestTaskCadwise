using SecondTestTask.Models.Atm;
using SecondTestTask.Services;
using SecondTestTask.ViewModels;

namespace SecondTestTask.Commands;

public class BackToLoginViewCommand : CommandBase
{
    private AtmController _atm;
    private NavigationService _navigationService;

    private OperationsViewModel _vm;

    public BackToLoginViewCommand(OperationsViewModel vm, AtmController atm, NavigationService navigationService)
    {
        _vm = vm;
        _atm = atm;
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        _atm.ExtractCard();
        _navigationService.Navigate();
    }
}