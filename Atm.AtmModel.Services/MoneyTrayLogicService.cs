using Atm.AtmModel.Interfaces;

namespace Atm.AtmModel.Services;

/// <summary>
/// Provides logic for withdrawing banknotes of a certain denomination
/// </summary>
public class MoneyTrayLogicService
{
    private IMoneyTray[] _moneyTrays;
    private decimal _amount;

    // TODO(narzaru) class can be rewritten to pattern strategy for different withdrawal offers
    public MoneyTrayLogicService(IEnumerable<IMoneyTray> moneyTrays)
    {
        _moneyTrays = moneyTrays.ToArray();
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
        var result =
            from moneyStack in moneyStacks
            let moneyTray = _moneyTrays.FindByDenomination(moneyStack.Denomination)
            where moneyTray is not null
            let allowableQuantity = Math.Min(moneyTray.RemainingPlaces(), moneyStack.Quantity)
            where allowableQuantity > 0
            select moneyStack with { Quantity = allowableQuantity };

        offerMoneyStacks = result;
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
    public bool TryCreateACashWithdrawOffer(
        decimal requiredAmount,
        decimal denomination,
        out IEnumerable<MoneyStack> possibleWithdraw)
    {
        possibleWithdraw = new List<MoneyStack>();

        if (_moneyTrays.IsNoMoney() || requiredAmount > _moneyTrays.MoneyAmount())
            return false;

        _amount = requiredAmount;
        var moneyStacks = new List<MoneyStack>();
        moneyStacks.Add(GiveMainDenomination(denomination));
        moneyStacks.AddRange(GiveApproximatedDomination());

        possibleWithdraw = moneyStacks.Where(moneyStack => moneyStack.Quantity > 0);

        return possibleWithdraw.Any();
    }

    private MoneyStack GiveMainDenomination(decimal denomination)
    {
        var moneyTray = _moneyTrays.FindByDenomination(denomination);
        if (moneyTray is null) return new MoneyStack { Denomination = 0, Quantity = 0 };

        var targetNumberOfBanknotes = decimal.ToInt32(Math.Floor(_amount / moneyTray!.BanknoteDenomination));
        var realNumberOfBanknote = Math.Min(targetNumberOfBanknotes, moneyTray.NumberOfBanknotes);
        _amount -= moneyTray.BanknoteDenomination * realNumberOfBanknote;
        moneyTray.DecreaseNumberOfBanknotes(realNumberOfBanknote);

        return new MoneyStack { Denomination = denomination, Quantity = realNumberOfBanknote };
    }

    private IEnumerable<MoneyStack> GiveApproximatedDomination()
    {
        return
            from moneyTray in _moneyTrays
            where moneyTray.NumberOfBanknotes > 0 && moneyTray.BanknoteDenomination <= _amount
            let moneyStack = GiveMainDenomination(moneyTray.BanknoteDenomination)
            where moneyStack.Quantity > 0
            select new MoneyStack
            {
                Denomination = moneyStack.Denomination,
                Quantity = moneyStack.Quantity
            };
    }
}