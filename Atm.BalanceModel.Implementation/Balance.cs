using System;
using Atm.BalanceModel.Interfaces;

namespace Atm.BalanceModel.Implementation;

public class Balance : IBalance
{
    private decimal _amount;

    public Balance(decimal initialBalance)
    {
        if (initialBalance < 0) throw new ArgumentException("Initial balance cannot be negative.");

        _amount = initialBalance;
    }

    public decimal GetMoneyAmount()
    {
        return _amount;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be greater than or equal to zero.");

        _amount += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.");

        if (amount > _amount) throw new InvalidOperationException("Insufficient funds.");

        _amount -= amount;
    }

    public IBalanceSnapshot MakeSnapshot()
    {
        return new BalanceSnapshot(_amount);
    }

    public void RestoreFromSnapshot(IBalanceSnapshot snapshot)
    {
        if (snapshot is not BalanceSnapshot concreteSnapshot)
            throw new InvalidCastException("Unable to get object of type BalanceSnapshot.");

        _amount = concreteSnapshot.Balance;
    }
}