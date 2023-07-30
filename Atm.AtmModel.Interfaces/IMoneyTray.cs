namespace Atm.AtmModel.Interfaces;

public static class MoneyTraysExtension
{
    public static IMoneyTray? FindByDenomination(this IEnumerable<IMoneyTray> moneyTrays, decimal denomination)
    {
        return moneyTrays.FirstOrDefault(moneyTray => moneyTray.BanknoteDenomination == denomination);
    }

    public static decimal MoneyAmount(this IEnumerable<IMoneyTray> moneyTrays)
    {
        return moneyTrays.Sum(mt => mt.NumberOfBanknotes * mt.BanknoteDenomination);
    }

    public static bool IsNoMoney(this IEnumerable<IMoneyTray> moneyTrays)
    {
        return moneyTrays.MoneyAmount() == 0;
    }
}

public static class MoneyTrayExtension
{
    public static int RemainingPlaces(this IMoneyTray moneyTray)
    {
        return moneyTray.NumberOfBanknotesLimit - moneyTray.NumberOfBanknotes;
    }

    public static decimal MoneyAmount(this IMoneyTray moneyTray)
    {
        return moneyTray.BanknoteDenomination * moneyTray.NumberOfBanknotes;
    }
}

public interface IMoneyTray
{
    public decimal BanknoteDenomination { get; }

    public int NumberOfBanknotes { get; }

    public int NumberOfBanknotesLimit { get; }

    public void IncreaseNumberOfBanknotes(int amount);

    public void DecreaseNumberOfBanknotes(int amount);
}