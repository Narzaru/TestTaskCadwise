namespace Atm.UserAccountModel.Implementation;

public class BankCardBase
{
    public BankCardBase(ulong uid)
    {
        Uid = uid;
    }

    public ulong Uid { get; private set; }
}