using Atm.AtmModel.Interfaces;
using Atm.AtmModel.Services;

namespace Atm.AtmModel.Implementation;

public class MoneyTray : IMoneyTray
{
    public MoneyTray(decimal banknoteDenomination, int numberOfBanknotes, int numberOfBanknotesLimit)
    {
        BanknoteDenomination = banknoteDenomination;
        NumberOfBanknotes = numberOfBanknotes;
        NumberOfBanknotesLimit = numberOfBanknotesLimit;
    }

    public decimal BanknoteDenomination { get; private set; }
    public int NumberOfBanknotes { get; private set; }
    public int NumberOfBanknotesLimit { get; private set; }

    public void IncreaseNumberOfBanknotes(int count)
    {
        if (NumberOfBanknotes + count > NumberOfBanknotesLimit)
            throw new InvalidOperationException("Banknote limit exceeded");

        NumberOfBanknotes += count;
    }

    public void DecreaseNumberOfBanknotes(int count)
    {
        if (NumberOfBanknotes - count < 0)
            throw new InvalidOperationException("Banknote limit exceeded");

        NumberOfBanknotes -= count;
    }
}