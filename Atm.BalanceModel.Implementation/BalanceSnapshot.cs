﻿using Atm.BalanceModel.Interfaces;

namespace Atm.BalanceModel.Implementation;

public class BalanceSnapshot : IBalanceSnapshot
{
    public BalanceSnapshot(decimal balance)
    {
        Balance = balance;
    }

    public decimal Balance { get; private set; }
}