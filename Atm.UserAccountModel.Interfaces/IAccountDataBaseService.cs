using Atm.UserAccountModel.Implementation;

namespace Atm.UserAccountModel.Interfaces;

public interface IAccountDataBaseService
{
    bool TryAuthenticate(ulong bankCardUid, string pinCode, out BankAccountBase? account);
    bool TryUpdateAccount(BankAccountBase account);
}