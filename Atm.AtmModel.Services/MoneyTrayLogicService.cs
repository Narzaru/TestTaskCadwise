using Atm.AtmModel.Interfaces;

namespace Atm.AtmModel.Services;

public static class MoneyTraysExtension
{
    public static decimal MoneyAmount(this IEnumerable<IMoneyTray> moneyTrays)
    {
        return moneyTrays.Sum(mt => mt.NumberOfBanknotes * mt.BanknoteDenomination);
    }

    public static int RemainingPlaces(this IMoneyTray moneyTray)
    {
        return moneyTray.NumberOfBanknotesLimit - moneyTray.NumberOfBanknotes;
    }
}

public struct MoneyStack
{
    public decimal Denomination;
    public int Quantity;
}

/// <summary>
/// Provides logic for withdrawing funds of a certain denomination
/// </summary>
public class MoneyTrayLogicService
{
    private IEnumerable<IMoneyTray> _moneyTrays;
    private IEnumerable<IMoneyTray> _moneyTraysCopy = null!;
    private decimal _amount;

    // TODO(narzaru) class can be rewritten to pattern strategy
    public MoneyTrayLogicService(IEnumerable<IMoneyTray> moneyTrays)
    {
        _moneyTrays = moneyTrays;
    }

    /// <summary>
    /// Will try to create a deposit offer
    /// </summary>
    /// <param name="moneyStacks">
    /// inserted money
    /// </param>
    /// <param name="offerMoneyStacks">
    /// accepted money
    /// </param>
    /// <returns>
    /// if any offer was made return true
    /// </returns>
    public bool TryCreateACashDepositOffer(
        IEnumerable<MoneyStack> moneyStacks,
        out IEnumerable<MoneyStack> offerMoneyStacks)
    {
        var list = new List<MoneyStack>();
        foreach (var moneyStack in moneyStacks)
        {
            var moneyTray = _moneyTrays.FirstOrDefault(mt => mt.BanknoteDenomination == moneyStack.Denomination);
            if (moneyTray is not null)
                list.Add(InsertMoneyStack(moneyTray, moneyStack));
        }

        offerMoneyStacks = list;
        return offerMoneyStacks.Any();
    }

    /// <summary>
    /// Will try to create a withdraw offer
    /// </summary>
    /// <param name="requiredAmount">
    /// Amount to be withdrawn
    /// </param>
    /// <param name="denomination">
    /// Preferred denomination
    /// </param>
    /// <param name="possibleWithdraw">
    /// Offer as close as possible to the entered amount
    /// </param>
    /// <returns>
    /// If any offer was made return true
    /// </returns>
    public bool TryCreateACashWithdrawOffer(decimal requiredAmount, decimal denomination,
        out IEnumerable<MoneyStack> possibleWithdraw)
    {
        _moneyTraysCopy = _moneyTrays.ToArray();
        _amount = requiredAmount;
        possibleWithdraw = new List<MoneyStack>();

        if (_moneyTraysCopy.MoneyAmount() == 0 || _amount > _moneyTraysCopy.MoneyAmount())
            return false;

        if (_moneyTraysCopy.FirstOrDefault(mt => mt.BanknoteDenomination == denomination) is null)
            return false;

        var moneyStacks = new List<MoneyStack>();
        moneyStacks.Add(GiveMainDenomination(denomination));
        moneyStacks.AddRange(GiveApproximatedDomination(denomination));

        possibleWithdraw = moneyStacks.Where(ms => ms.Quantity > 0);

        return possibleWithdraw.Any();
    }

    private MoneyStack GiveMainDenomination(decimal denomination)
    {
        var moneyTray = _moneyTraysCopy.FirstOrDefault(mt => mt.BanknoteDenomination == denomination);
        var moneyStacks = new MoneyStack
        {
            Denomination = denomination,
            Quantity = 0
        };

        var targetNumberOfBanknotes = decimal.ToInt32(Math.Floor(_amount / moneyTray!.BanknoteDenomination));
        var realNumberOfBanknote = targetNumberOfBanknotes > moneyTray.NumberOfBanknotes
            ? moneyTray.NumberOfBanknotes
            : targetNumberOfBanknotes;

        if (realNumberOfBanknote == 0)
            return moneyStacks;

        moneyStacks = new MoneyStack
        {
            Denomination = denomination,
            Quantity = realNumberOfBanknote
        };
        _amount -= moneyTray.BanknoteDenomination * realNumberOfBanknote;
        moneyTray.DecreaseNumberOfBanknotes(realNumberOfBanknote);

        return moneyStacks;
    }

    private IEnumerable<MoneyStack> GiveApproximatedDomination(decimal denomination)
    {
        var moneyStacks = new List<MoneyStack>();

        var availableMoneyTrays = _moneyTraysCopy
            .Where(mt => mt.NumberOfBanknotes > 0 && mt.BanknoteDenomination <= _amount)
            .OrderByDescending(mt => mt.BanknoteDenomination);

        foreach (var moneyTray in availableMoneyTrays)
        {
            moneyStacks.Add(GiveMainDenomination(moneyTray.BanknoteDenomination));
        }

        return moneyStacks;
    }

    private MoneyStack InsertMoneyStack(IMoneyTray moneyTray, MoneyStack moneyStack)
    {
        var allowableQuantity = moneyTray.RemainingPlaces() > moneyStack.Quantity
            ? moneyStack.Quantity
            : moneyTray.RemainingPlaces();

        moneyTray.IncreaseNumberOfBanknotes(allowableQuantity);
        return moneyStack with { Quantity = allowableQuantity };
    }
}