namespace SecondTestTask.Models.Balance;

public class BalanceSnapshot : IBalanceSnapshot
{
    public BalanceSnapshot(decimal balance)
    {
        Balance = balance;
    }

    public decimal Balance { get; private set; }
}