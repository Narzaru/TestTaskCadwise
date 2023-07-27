﻿using SecondTestTask.Models.Atm;
using SecondTestTask.Services;
using SecondTestTask.ViewModels;

namespace SecondTestTask.Commands;

public class AddMoneyCommand : CommandBase
{
    private AtmController m_atm;
    private NavigationService m_toInvalidOperation;
    private NavigationService m_toOperationsMenu;
    private DepositViewModel m_vm;

    public AddMoneyCommand(DepositViewModel vm, AtmController atm, NavigationService navigateToOperationsMenu,
        NavigationService toInvalidOperation)
    {
        m_vm = vm;
        m_atm = atm;
        m_toInvalidOperation = toInvalidOperation;
        m_toOperationsMenu = navigateToOperationsMenu;
    }

    public override void Execute(object? parameter)
    {
        var cashService = m_atm.GetCashTransactionService();
        try
        {
            if (!cashService.TryDeposit(decimal.Parse(m_vm.AmountOfMoney)))
            {
                m_toInvalidOperation.Navigate();
                m_atm.ExtractCard();
                return;
            }

            m_toOperationsMenu.Navigate();
        }
        catch
        {
            m_toInvalidOperation.Navigate();
            m_atm.ExtractCard();
        }
    }
}