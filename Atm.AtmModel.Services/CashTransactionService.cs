using Atm.UserAccountModel.Implementation;
using Atm.UserAccountModel.Interfaces;

namespace Atm.AtmModel.Services;

/// <summary>
/// Represents the logic for replenishing the account balance and withdrawing funds
/// </summary>
public class CashTransactionService
{
    private BankAccountBase _account;
    private IAccountDataBaseService _accountService;

    public CashTransactionService(BankAccountBase account, IAccountDataBaseService accountDataBase)
    {
        _account = account ?? throw new ArgumentNullException(nameof(account));
        _accountService = accountDataBase ?? throw new ArgumentNullException(nameof(accountDataBase));
    }

    public void Withdraw(decimal amount)
    {
        var snapshot = _account.Balance.MakeSnapshot();
        _account.Balance.Withdraw(amount);
        try
        {
            _accountService.TryUpdateAccount(_account);
        }
        catch
        {
            _account.Balance.RestoreFromSnapshot(snapshot);
            throw;
        }
    }

    public void Deposit(decimal amount)
    {
        var snapshot = _account.Balance.MakeSnapshot();
        _account.Balance.Deposit(amount);
        try
        {
            _accountService.TryUpdateAccount(_account);
        }
        catch
        {
            _account.Balance.RestoreFromSnapshot(snapshot);
            throw;
        }
    }

    public decimal Balance()
    {
        return _account.Balance.GetMoneyAmount();
    }
}