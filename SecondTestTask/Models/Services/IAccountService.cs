using SecondTestTask.Models.Account;

namespace SecondTestTask.Models.Services;

public interface IAccountService
{
    public IAccount? Authenticate(ulong id, string inputCode);
    public bool TryUpdateAccount(IAccount account);
}