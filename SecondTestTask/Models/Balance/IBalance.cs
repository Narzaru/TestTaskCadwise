namespace SecondTestTask.Models.Balance;

public interface IBalance
{
    public decimal GetCurrentBalance();

    public void Deposit(decimal amount);

    public void Withdraw(decimal amount);

    public IBalanceSnapshot MakeSnapshot();

    public void RestoreFromSnapshot(IBalanceSnapshot snapshot);
}