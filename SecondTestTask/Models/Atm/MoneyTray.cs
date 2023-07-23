using System;

namespace SecondTestTask.Models.Atm;

public class MoneyTray
{
    public MoneyTray(decimal banknoteDenomination, decimal numberOfBanknotes, decimal numberOfBanknotesLimit)
    {
        BanknoteDenomination = banknoteDenomination;
        NumberOfBanknotes = numberOfBanknotes;
        NumberOfBanknotesLimit = BanknoteDenomination;
    }

    public decimal BanknoteDenomination { get; private set; }
    public decimal NumberOfBanknotes { get; private set; }
    public decimal NumberOfBanknotesLimit { get; private set; }

    public void IncreaseNumberOfBanknotes()
    {
        if (NumberOfBanknotes + 1 > NumberOfBanknotesLimit)
        {
            throw new Exception("Banknote limit exceeded");
        }

        ++NumberOfBanknotes;
    }

    private void DecreaseNumberOfBanknotes()
    {
        if (NumberOfBanknotes - 1 < 0)
        {
            throw new Exception("Banknote limit exceeded");
        }

        --NumberOfBanknotes;
    }
}