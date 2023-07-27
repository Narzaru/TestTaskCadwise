using Atm.AtmModel.Interfaces;

namespace Atm.AtmModel.Implementation;

public class MoneyTray : IMoneyTray
{
    public MoneyTray(decimal banknoteDenomination, decimal numberOfBanknotes, decimal numberOfBanknotesLimit)
    {
        BanknoteDenomination = banknoteDenomination;
        NumberOfBanknotes = numberOfBanknotes;
        NumberOfBanknotesLimit = numberOfBanknotesLimit;
    }

    public decimal BanknoteDenomination { get; private set; }
    public decimal NumberOfBanknotes { get; private set; }
    public decimal NumberOfBanknotesLimit { get; private set; }

    public void IncreaseNumberOfBanknotes(int amount)
    {
        if (NumberOfBanknotes + amount > NumberOfBanknotesLimit)
            throw new InvalidOperationException("Banknote limit exceeded");

        NumberOfBanknotes += amount;
    }

    public void DecreaseNumberOfBanknotes(int amount)
    {
        if (NumberOfBanknotes - amount < 0)
            throw new InvalidOperationException("Banknote limit exceeded");

        NumberOfBanknotes -= amount;
    }
}