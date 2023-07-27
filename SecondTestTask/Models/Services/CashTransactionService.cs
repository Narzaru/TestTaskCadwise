using System;
using SecondTestTask.Models.Account;

namespace SecondTestTask.Models.Services;

public class CashTransactionService
{
    private IAccount m_account;
    private IAccountService m_accountService;

    public CashTransactionService(IAccount account, IAccountService accountService)
    {
        m_account = account ?? throw new ArgumentNullException(nameof(account));
        m_accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
    }

    public bool TryDeposit(decimal amount)
    {
        try
        {
            m_account.Deposit(amount);
            if (!m_accountService.TryUpdateAccount(m_account))
            {
                m_account.CancelOperation();
                return false;
            }

            return true;
        }
        catch
        {
            m_account.CancelOperation();
        }

        return false;
    }

    public bool TryWithdraw(decimal amount)
    {
        try
        {
            m_account.Withdraw(amount);
            if (!m_accountService.TryUpdateAccount(m_account))
            {
                m_account.CancelOperation();
                return false;
            }

            return true;
        }
        catch
        {
            m_account.CancelOperation();
        }

        return false;
    }

    public decimal Balance()
    {
        return m_account.GetBalance();
    }
}