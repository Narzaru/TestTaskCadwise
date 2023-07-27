using System;
using System.Collections.Generic;
using System.Linq;
using SecondTestTask.Models.Balance;

namespace SecondTestTask.Models.Account;

public class AccountBase : IAccount
{
    private IBalance m_balance;
    private List<IBalanceSnapshot> m_balanceSnapshot;
    private ulong m_token;

    private ulong m_uid;

    public AccountBase(ulong accountId, ulong token, IBalance balance)
    {
        m_token = token;
        m_uid = accountId;
        m_balance = balance;
        m_balanceSnapshot = new List<IBalanceSnapshot>();
    }

    public void Deposit(decimal amount)
    {
        m_balanceSnapshot.Add(m_balance.MakeSnapshot());
        m_balance.Deposit(amount);
    }

    public void Withdraw(decimal amount)
    {
        m_balanceSnapshot.Add(m_balance.MakeSnapshot());
        m_balance.Withdraw(amount);
    }

    public void CancelOperation()
    {
        if (!m_balanceSnapshot.Any()) throw new FieldAccessException("Operation stack is empty");

        var snapshot = m_balanceSnapshot.Last();
        m_balance.RestoreFromSnapshot(snapshot);
        m_balanceSnapshot.Remove(snapshot);
    }

    public decimal GetBalance()
    {
        return m_balance.GetCurrentBalance();
    }

    public ulong GetAccountId()
    {
        return m_uid;
    }

    public ulong GetAccountTempToken()
    {
        return m_token;
    }
}