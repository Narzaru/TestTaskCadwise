using Atm.BalanceModel.Interfaces;

namespace Atm.UserAccountModel.Implementation;

public class BankAccountBase
{
    public BankAccountBase(ulong accountId, IBalance balance)
    {
        Uid = accountId;
        Balance = balance;
    }

    public IBalance Balance { get; private set; }

    public ulong Uid { get; private set; }
}