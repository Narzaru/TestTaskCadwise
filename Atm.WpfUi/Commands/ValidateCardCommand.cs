using Atm.AtmModel.Implementation;
using Atm.UserAccountModel.Implementation;
using Atm.WpfUi.Services;
using Atm.WpfUi.ViewModels;

namespace Atm.WpfUi.Commands;

public class ValidateCardCommand : CommandBase
{
    private AtmController _atm;
    private NavigationService _navigateToError;
    private NavigationService _navigateToOperation;

    private LoginViewModel _vm;

    public ValidateCardCommand(LoginViewModel vm, AtmController atm, NavigationService navigateToOperation,
        NavigationService navigateToError)
    {
        _vm = vm;
        _atm = atm;
        _navigateToOperation = navigateToOperation;
        _navigateToError = navigateToError;
    }

    public override void Execute(object? parameter)
    {
        try
        {
            _atm.InsertCard(new BankCardBase(ulong.Parse(_vm.CardId)));
        }
        catch
        {
            _navigateToError.Navigate();
            return;
        }

        try
        {
            _atm.InputCode(_vm.Code);
        }
        catch
        {
            _atm.ExtractCard();
            _navigateToError.Navigate();
            return;
        }

        _navigateToOperation.Navigate();
    }
}