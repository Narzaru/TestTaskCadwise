using Atm.UserAccountModel.Implementation;
using Atm.UserAccountModel.Interfaces;

namespace Atm.AtmModel.Services;

public class CashTransactionService
{
    private BankAccountBase _account;
    private IAccountDataBaseService _accountService;

    public CashTransactionService(BankAccountBase account, IAccountDataBaseService accountDataBaseService)
    {
        _account = account ?? throw new ArgumentNullException(nameof(account));
        _accountService = accountDataBaseService ?? throw new ArgumentNullException(nameof(accountDataBaseService));
    }
}