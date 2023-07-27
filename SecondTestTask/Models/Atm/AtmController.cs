using System;
using System.Collections.Generic;
using SecondTestTask.Models.Account;
using SecondTestTask.Models.Services;

namespace SecondTestTask.Models.Atm;

public class AtmController
{
    private IAccount? m_account;
    private IAccountService m_accountService;
    private SortedSet<ulong> m_allowedBanknoteSize;
    private Card? m_insertedCard;
    private List<MoneyTray> m_moneyTrays;

    public AtmController(IAccountService accountService)
    {
        m_allowedBanknoteSize = new SortedSet<ulong>
        {
            10, 50, 100, 200, 500, 1000, 2000, 5000
        };
        m_moneyTrays = new List<MoneyTray>();
        foreach (var banknoteSize in m_allowedBanknoteSize) m_moneyTrays.Add(new MoneyTray(banknoteSize, 10, 15));

        m_accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        m_insertedCard = null;
    }

    public void InsertCard(Card card)
    {
        if (m_insertedCard is not null) throw new ArgumentException("Card already inserted");

        m_insertedCard = card;
    }

    public Card? ExtractCard()
    {
        var card = m_insertedCard;
        m_insertedCard = null;
        return card;
    }

    public void GiveMoney(decimal amount)
    {
        // add logic with money tray
        throw new NotImplementedException();
    }

    public void GetMoney(decimal amount)
    {
        // remove logic with money tray
        throw new NotImplementedException();
    }

    public void InputCode(string code)
    {
        if (m_insertedCard is null)
            throw new Exception("Card is not inserted.");

        m_account = m_accountService.Authenticate(m_insertedCard.Id, code);

        if (m_account is null)
            throw new Exception("Authentication error.");
    }

    public CashTransactionService GetCashTransactionService()
    {
        if (m_account is null)
            throw new Exception("Account not found");

        return new CashTransactionService(m_account, m_accountService);
    }
}