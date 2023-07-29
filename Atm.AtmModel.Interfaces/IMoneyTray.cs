namespace Atm.AtmModel.Interfaces;

public interface IMoneyTray
{
    public decimal BanknoteDenomination { get; }

    public int NumberOfBanknotes { get; }

    public int NumberOfBanknotesLimit { get; }

    public void IncreaseNumberOfBanknotes(int amount);

    public void DecreaseNumberOfBanknotes(int amount);
}