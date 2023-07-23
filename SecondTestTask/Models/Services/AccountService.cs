using System;
using System.Collections.Generic;
using SecondTestTask.Models.Account;

namespace SecondTestTask.Models.Services;

public class AccountService : IAccountService
{
    public AccountService()
    {
        m_accounts = new SortedDictionary<ulong, string>
        {
            { 1, "1234" },
            { 2, "1234" },
            { 3, "1234" }
        };

        m_balance = new SortedDictionary<ulong, decimal>
        {
            { 1, 10000 },
            { 2, 20000 },
            { 3, 30000 }
        };
    }

    public IAccount? Authenticate(ulong id, string inputCode)
    {
        if (inputCode == null) throw new ArgumentNullException(nameof(inputCode));

        if (m_accounts.TryGetValue(id, out var accountCode))
        {
            if (accountCode == inputCode)
            {
                return new AccountBase(id, id, new Balance.Balance(m_balance[id]));
            }
        }

        return null;
    }

    public bool TryUpdateAccount(IAccount account)
    {
        if (account.GetAccountId() == account.GetAccountTempToken())
        {
            try
            {
                m_balance[account.GetAccountId()] = account.GetBalance();
                return true;
            }
            catch
            {
                return false;
            }
        }

        return false;
    }

    private SortedDictionary<ulong, string> m_accounts;
    private SortedDictionary<ulong, decimal> m_balance;
}