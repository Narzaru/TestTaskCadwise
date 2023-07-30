using Atm.AtmModel.Implementation;
using Atm.AtmModel.Services;
using Atm.BalanceModel.Implementation;
using Atm.UserAccountModel.Implementation;
using Atm.UserAccountModel.Interfaces;
using Moq;

namespace Atm.AtmModel.Tests;

public class AtmControllerTests
{
    private delegate bool GetAccount(ulong bankCardUid, string pinCode, out BankAccountBase? account);

    private delegate bool UpdateAccount(ulong bankCardUid, string pinCode, out BankAccountBase? account);

    private static IAccountDataBaseService GetMockDb()
    {
        BankAccountBase? account = null;
        var dbService = new Mock<IAccountDataBaseService>();
        dbService
            .Setup(db => db.TryAuthenticate(It.IsAny<ulong>(), It.IsAny<string>(), out account))
            .Returns(new GetAccount(
                (ulong id, string pin, out BankAccountBase? accountBase) =>
                {
                    accountBase = new BankAccountBase(id, new Balance(10000));
                    return true;
                }
            ));

        dbService.Setup(db => db.TryUpdateAccount(It.IsAny<BankAccountBase>()))
            .Returns(() => true);

        return dbService.Object;
    }

    [Fact]
    public void InsertMoneyStacks_NormalMoneyInsert_AllMoneyInserted()
    {
        var atm = new AtmController(GetMockDb(), 29, 30);
        atm.InsertCard(new BankCardBase(0));
        atm.InputCode("");

        var returnedUserMoney = atm.InsertMoneyStacks(new MoneyStack[]
        {
            new() { Denomination = 100, Quantity = 1 },
            new() { Denomination = 50, Quantity = 1 },
            new() { Denomination = 200, Quantity = 1 },
        });

        Assert.Empty(returnedUserMoney);
    }

    [Fact]
    public void InsertMoneyStacks_ExtraMoneyInsert_NotAllMoneyInserted()
    {
        var atm = new AtmController(GetMockDb(), 20, 30);
        atm.InsertCard(new BankCardBase(0));
        atm.InputCode("");

        // overflow money trays
        var returnedUserMoney = atm.InsertMoneyStacks(new MoneyStack[]
        {
            new() { Denomination = 100, Quantity = 200 },
            new() { Denomination = 50, Quantity = 200 },
            new() { Denomination = 200, Quantity = 200 }
        });

        // try insert into overflow money trays
        returnedUserMoney = atm.InsertMoneyStacks(new MoneyStack[]
        {
            new() { Denomination = 100, Quantity = 200 },
            new() { Denomination = 50, Quantity = 200 },
            new() { Denomination = 200, Quantity = 200 }
        });

        var expected = new MoneyStack[]
        {
            new() { Denomination = 100, Quantity = 190 },
            new() { Denomination = 50, Quantity = 190 },
            new() { Denomination = 200, Quantity = 190 }
        };

        Assert.Equal(expected, returnedUserMoney);
    }

    [Fact]
    public void PullMoneyStacks_ExtraMoneyPull_NotAllMoneyPulled()
    {
        var atm = new AtmController(GetMockDb(), 20, 30);
        atm.InsertCard(new BankCardBase(0));
        atm.InputCode("");

        var returnedUserMoney = atm.PullMoneyStacks(10000000, 200);
        Assert.Empty(returnedUserMoney.Key);
    }
}