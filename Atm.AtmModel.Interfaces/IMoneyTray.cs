namespace Atm.AtmModel.Interfaces;

public interface IMoneyTray
{
    public decimal BanknoteDenomination { get; }

    public decimal NumberOfBanknotes { get; }

    public decimal NumberOfBanknotesLimit { get; }

    public void IncreaseNumberOfBanknotes(int amount);

    public void DecreaseNumberOfBanknotes(int amount);
}