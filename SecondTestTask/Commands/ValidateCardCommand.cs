using System;
using SecondTestTask.Models.Account;
using SecondTestTask.Models.Atm;
using SecondTestTask.Services;
using SecondTestTask.ViewModels;

namespace SecondTestTask.Commands;

public class ValidateCardCommand : CommandBase
{
    public ValidateCardCommand(LoginViewModel vm, AtmController atm, NavigationService navigateToOperation, NavigationService navigateToError)
    {
        m_vm = vm;
        m_atm = atm;
        m_navigateToOperation = navigateToOperation;
        m_navigateToError = navigateToError;
    }

    public override void Execute(object? parameter)
    {
        try
        {
            m_atm.InsertCard(new Card(ulong.Parse(m_vm.CardId)));
        }
        catch
        {
            m_navigateToError.Navigate();
            return;
        }

        try
        {
            m_atm.InputCode(m_vm.Code);
        }
        catch
        {
            m_atm.ExtractCard();
            m_navigateToError.Navigate();
            return;
        }

        m_navigateToOperation.Navigate();
    }

    private LoginViewModel m_vm;
    private AtmController m_atm;
    private NavigationService m_navigateToOperation;
    private NavigationService m_navigateToError;
}