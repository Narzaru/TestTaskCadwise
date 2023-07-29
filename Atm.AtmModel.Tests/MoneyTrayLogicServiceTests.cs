using Atm.AtmModel.Implementation;
using Atm.AtmModel.Interfaces;
using Atm.AtmModel.Services;

namespace Atm.AtmModel.Tests;

public class MoneyTrayLogicServiceTests
{
    public IEnumerable<IMoneyTray> CreateMoneyTrays(int numberOfBanknotes, int numberOfBanknotesLimit)
    {
        return new[] { 50, 100, 200, 500, 1000, 2000, 5000 }
            .Select(banknoteSize =>
                new MoneyTray(banknoteSize, numberOfBanknotes, numberOfBanknotesLimit));
    }

    // TODO(narzaru) rename tests
    [Fact]
    public void _()
    {
        MoneyTrayLogicService moneyTrayLogicService = new(CreateMoneyTrays(2, 10));

        moneyTrayLogicService.TryCreateACashWithdrawOffer(11111, 200, out var actual);
        var expected = new List<MoneyStack>
        {
            new() { Denomination = 200, Quantity = 2 },
            new() { Denomination = 5000, Quantity = 2 },
            new() { Denomination = 500, Quantity = 1 },
            new() { Denomination = 100, Quantity = 2 }
        };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void _1()
    {
        MoneyTrayLogicService moneyTrayLogicService = new(CreateMoneyTrays(9, 10));

        moneyTrayLogicService.TryCreateACashDepositOffer(new MoneyStack[]
        {
            new() { Denomination = 50, Quantity = 5 },
            new() { Denomination = 200, Quantity = 5 }
        }, out var actual);

        var expected = new List<MoneyStack>
        {
            new() { Denomination = 50, Quantity = 1 },
            new() { Denomination = 200, Quantity = 1 }
        };

        Assert.Equal(expected, actual);
    }
}