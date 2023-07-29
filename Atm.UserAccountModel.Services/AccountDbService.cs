using Atm.BalanceModel.Implementation;
using Atm.UserAccountModel.Implementation;
using Atm.UserAccountModel.Interfaces;

namespace Atm.UserAccountModel.Services;

public class AccountDbService : IAccountDataBaseService
{
    private SortedDictionary<ulong, string> _accounts;
    private SortedDictionary<ulong, decimal> _balance;

    public AccountDbService()
    {
        _accounts = new SortedDictionary<ulong, string>
        {
            { 1, "1234" },
            { 2, "1234" },
            { 3, "1234" }
        };

        _balance = new SortedDictionary<ulong, decimal>
        {
            { 1, 10000 },
            { 2, 20000 },
            { 3, 30000 }
        };
    }

    public bool TryAuthenticate(ulong bankCardUid, string pinCode, out BankAccountBase? account)
    {
        account = Authenticate(bankCardUid, pinCode);
        return account is not null;
    }

    public bool TryUpdateAccount(BankAccountBase account)
    {
        if (!VerifyAccountConsistency(account.Uid)) return false;

        _balance[account.Uid] = account.Balance.GetMoneyAmount();
        return true;
    }

    public BankAccountBase? Authenticate(ulong uid, string inputCode)
    {
        return VerifyAccountConsistency(uid) ? new BankAccountBase(uid, new Balance(_balance[uid])) : null;
    }

    public bool VerifyAccountConsistency(ulong accountUid)
    {
        var accountExists = _accounts.ContainsKey(accountUid);
        var accountBalanceExists = _accounts.ContainsKey(accountUid);
        var balancePositive = _balance[accountUid] >= 0;

        return accountExists && accountBalanceExists && balancePositive;
    }
}