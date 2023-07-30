using Atm.AtmModel.Implementation;
using Atm.WpfUi.Services;
using Atm.WpfUi.ViewModels;

namespace Atm.WpfUi.Commands;

public class OperationsViewBackToLoginViewCommand : CommandBase
{
    private AtmController _atm;
    private NavigationService _navigationService;

    private OperationsViewModel _vm;

    public OperationsViewBackToLoginViewCommand(OperationsViewModel vm, AtmController atm, NavigationService navigationService)
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