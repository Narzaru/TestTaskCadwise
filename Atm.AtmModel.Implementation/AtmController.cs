﻿using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Atm.AtmModel.Interfaces;
using Atm.AtmModel.Services;
using Atm.UserAccountModel.Implementation;
using Atm.UserAccountModel.Interfaces;

namespace Atm.AtmModel.Implementation;

public class AtmController : INotifyPropertyChanged
{
    private BankCardBase? _insertedCard;
    private BankAccountBase? _account;
    private IAccountDataBaseService _accountDbService;
    private List<MoneyTray> _moneyTrays;
    private CashTransactionService? _cashTransactionService;

    public AtmController(
        IAccountDataBaseService accountService,
        int numberOfBanknotes = 20,
        int numberOfBanknotesLimit = 30)
    {
        _accountDbService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        _insertedCard = null;

        _moneyTrays = new[] { 50, 100, 200, 500, 1000, 2000, 5000 }
            .Select(banknoteSize => new MoneyTray(banknoteSize, numberOfBanknotes, numberOfBanknotesLimit))
            .ToList();
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
        _cashTransactionService = null;
        return card;
    }

    public void InputCode(string code)
    {
        if (_insertedCard is null)
            throw new InvalidOperationException("Card is not inserted.");

        if (!_accountDbService.TryAuthenticate(_insertedCard.Uid, code, out _account))
            throw new InvalidOperationException("Authentication error.");

        _cashTransactionService =
            new CashTransactionService(_account!, _accountDbService);
    }

    /// <summary>
    /// Tries to accept money inserted by the user
    /// </summary>
    /// <param name="moneyStacks">
    /// Money inserted by the user</param>
    /// <returns>
    /// Not accepted banknotes
    /// </returns>
    public IEnumerable<MoneyStack> InsertMoneyStacks(IEnumerable<MoneyStack> moneyStacks)
    {
        if (_cashTransactionService is null)
            throw new ArgumentNullException(
                nameof(_cashTransactionService),
                "Attempt to deposit an account without authorization");

        IEnumerable<MoneyStack> moneyStacksCopy = moneyStacks.ToList();

        var moneyTrayLogic = new MoneyTrayLogicService(_moneyTrays);
        if (moneyTrayLogic.TryCreateACashDepositOffer(moneyStacksCopy, out var acceptedMoney))
        {
            var offerMoneyStacks = acceptedMoney.ToList();

            foreach (var moneys in offerMoneyStacks)
            {
                var moneyTray = _moneyTrays
                    .FirstOrDefault(moneyTray => moneyTray.BanknoteDenomination == moneys.Denomination);

                if (moneyTray is not null)
                {
                    moneyTray.IncreaseNumberOfBanknotes(moneys.Quantity);
                    OnPropertyChanged(nameof(Moneys));
                }
            }

            _cashTransactionService.Deposit(offerMoneyStacks.Sum(moneyStack =>
                moneyStack.Denomination * moneyStack.Quantity));

            return moneyStacksCopy.Select(
                moneyStack =>
                {
                    var offerQuantity = offerMoneyStacks
                        .FirstOrDefault(moneyStackOffer => moneyStackOffer.Denomination == moneyStack.Denomination)
                        .Quantity;
                    return moneyStack with { Quantity = moneyStack.Quantity - offerQuantity };
                }).Where(moneyStack => moneyStack.Quantity > 0);
        }

        return moneyStacksCopy;
    }

    /// <summary>
    /// Tries to accept money inserted by the user
    /// </summary>
    /// <param name="amount">
    /// Amount of money that user want to withdraw
    /// </param>
    /// <param name="denomination">
    /// Favorite denomination
    /// </param>
    /// <returns>
    /// Cash offer that the user can accept
    /// </returns>
    public KeyValuePair<IEnumerable<MoneyStack>, Operation> PullMoneyStacks(decimal amount, decimal denomination)
    {
        if (_cashTransactionService is null)
            throw new ArgumentNullException(
                nameof(_cashTransactionService),
                "Attempt to withdraw an account without authorization");

        if (amount > CheckBalance())
            return new KeyValuePair<IEnumerable<MoneyStack>, Operation>(Enumerable.Empty<MoneyStack>(), new Operation(
                () => { }, () => { }));

        var moneyTrayLogic = new MoneyTrayLogicService(_moneyTrays);
        if (moneyTrayLogic.TryCreateACashWithdrawOffer(amount, denomination, out var withdrawOffer))
        {
            var pullMoneyStacks = withdrawOffer.ToList();
            OnPropertyChanged(nameof(Moneys));

            return new KeyValuePair<IEnumerable<MoneyStack>, Operation>(pullMoneyStacks,
                new Operation(
                    () =>
                    {
                        _cashTransactionService.Withdraw(pullMoneyStacks.Sum(ms => ms.Denomination * ms.Quantity));
                    }, () => { }));
        }

        return new KeyValuePair<IEnumerable<MoneyStack>, Operation>(Enumerable.Empty<MoneyStack>(), new Operation(
            () => { }, () => { }));
    }

    public decimal CheckBalance()
    {
        if (_cashTransactionService is null)
            throw new ArgumentNullException(
                nameof(_cashTransactionService),
                "Attempt to check balance without authorization");

        return _cashTransactionService.Balance();
    }

    public IEnumerable<decimal> GetTraysDenomination()
    {
        return _moneyTrays.Select(tray => tray.BanknoteDenomination);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public IEnumerable<MoneyStack> Moneys =>
        _moneyTrays.Select(moneyTray => new MoneyStack
            { Denomination = moneyTray.BanknoteDenomination, Quantity = moneyTray.NumberOfBanknotes });
}