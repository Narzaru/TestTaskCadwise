using SecondTestTask.Models.Atm;
using SecondTestTask.Services;
using SecondTestTask.ViewModels;

namespace SecondTestTask.Commands;

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
            _atm.InsertCard(new Card(ulong.Parse(_vm.CardId)));
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