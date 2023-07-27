using System.Collections.Immutable;
using Atm.AtmModel.Services;
using Atm.UserAccountModel.Implementation;
using Atm.UserAccountModel.Interfaces;

namespace Atm.AtmModel.Implementation;

public class AtmController
{
    private BankCardBase? _insertedCard;
    private BankAccountBase? _account;
    private IAccountDataBaseService _accountDbService;
    private IEnumerable<MoneyTray> _moneyTrays;

    public AtmController(IAccountDataBaseService accountService)
    {
        _accountDbService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        _insertedCard = null;

        _moneyTrays = new[] { 50, 100, 200, 500, 1000, 2000, 5000 }
            .Select(banknoteSize =>
                new MoneyTray(banknoteSize, numberOfBanknotes: 20, numberOfBanknotesLimit: 30));
    }

    public void InsertCard(BankCardBase card)
    {
        if (_insertedCard is not null) throw new ArgumentException("Card already inserted");
        _insertedCard = card;
    }

    public BankCardBase? ExtractCard()
    {
        var card = _insertedCard;
        _insertedCard = null;
        return card;
    }

    public void InputCode(string code)
    {
        if (_insertedCard is null)
            throw new InvalidOperationException("Card is not inserted.");

        if (!_accountDbService.TryAuthenticate(_insertedCard.Uid, code, out _account))
            throw new InvalidOperationException("Authentication error.");
    }
}