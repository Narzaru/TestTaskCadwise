namespace SecondTestTask.Models.Account;

public interface IAccount
{
    public void Deposit(decimal amount);
    public void Withdraw(decimal amount);
    public void CancelOperation();
    public decimal GetBalance();
    public ulong GetAccountId();
    public ulong GetAccountTempToken();
}