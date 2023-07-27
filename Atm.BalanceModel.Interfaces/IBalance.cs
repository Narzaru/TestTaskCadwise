namespace Atm.BalanceModel.Interfaces;

public interface IBalance
{
    public decimal GetMoneyAmount();

    public void Deposit(decimal amount);

    public void Withdraw(decimal amount);

    public IBalanceSnapshot MakeSnapshot();

    public void RestoreFromSnapshot(IBalanceSnapshot snapshot);
}