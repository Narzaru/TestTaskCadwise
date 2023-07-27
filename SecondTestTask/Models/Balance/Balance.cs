using System;

namespace SecondTestTask.Models.Balance;

public class Balance : IBalance
{
    private decimal m_amount;

    public Balance(decimal initialBalance)
    {
        if (initialBalance < 0) throw new ArgumentException("Initial balance cannot be negative.");

        m_amount = initialBalance;
    }

    public decimal GetCurrentBalance()
    {
        return m_amount;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.");

        m_amount += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.");

        if (amount > m_amount) throw new InvalidOperationException("Insufficient funds.");

        m_amount -= amount;
    }

    public IBalanceSnapshot MakeSnapshot()
    {
        return new BalanceSnapshot(m_amount);
    }

    public void RestoreFromSnapshot(IBalanceSnapshot snapshot)
    {
        var concreteSnapshot = snapshot as BalanceSnapshot;
        if (concreteSnapshot == null)
            throw new InvalidCastException("Unable to access specific type of balance snapshot.");

        m_amount = concreteSnapshot.Balance;
    }
}