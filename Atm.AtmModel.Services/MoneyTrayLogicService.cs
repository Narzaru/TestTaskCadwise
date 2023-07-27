using Atm.AtmModel.Interfaces;

namespace Atm.AtmModel.Services;

public class MoneyTrayLogicService
{
    MoneyTrayLogicService(IEnumerable<IMoneyTray> moneyTrays, decimal amount)
    {
    }

    IEnumerable<int> WithdrawBySmallDenomination()
    {
        return Array.Empty<int>();
    }

    IEnumerable<int> WithdrawByLargeDenomination()
    {
        return Array.Empty<int>();
    }
}