﻿using System.Windows;
using Atm.AtmModel.Implementation;
using Atm.UserAccountModel.Services;
using Atm.WpfUi.Navigation;
using Atm.WpfUi.Services;
using Atm.WpfUi.ViewModels;
using Atm.WpfUi.Views;

namespace Atm.WpfUi;

public partial class App : Application
{
    private readonly AtmController _atmController;
    private readonly NavigationStore _navigationStore;

    public App()
    {
        _atmController = new AtmController(new AccountDbService());
        _navigationStore = new NavigationStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _navigationStore.CurrentViewModel = MakeLoginViewModel();

        var atmView = new AtmServiceView
        {
            DataContext = new AtmServiceViewModel(_atmController)
        };
        atmView.Show();

        MainWindow = new MainWindow
        {
            DataContext = new MainWindowViewModel(_navigationStore)
        };
        MainWindow.Show();


        base.OnStartup(e);
    }

    private OperationsViewModel MakeOperationsViewModel()
    {
        return new OperationsViewModel(_atmController,
            new NavigationService(_navigationStore, MakeLoginViewModel),
            new NavigationService(_navigationStore, MakeBalanceViewModel),
            new NavigationService(_navigationStore, MakeDepositViewModel),
            new NavigationService(_navigationStore, MakeWithdrawViewModel));
    }

    private LoginViewModel MakeLoginViewModel()
    {
        return new LoginViewModel(_atmController,
            new NavigationService(_navigationStore, MakeOperationsViewModel),
            new NavigationService(_navigationStore, MakeInvalidOperationView));
    }

    private InvalidOperationViewModel MakeInvalidOperationView()
    {
        return new InvalidOperationViewModel(new NavigationService(_navigationStore, MakeLoginViewModel));
    }

    private BalanceViewModel MakeBalanceViewModel()
    {
        return new BalanceViewModel(_atmController,
            new NavigationService(_navigationStore, MakeOperationsViewModel));
    }

    private DepositViewModel MakeDepositViewModel()
    {
        return new DepositViewModel(_atmController,
            new NavigationService(_navigationStore, MakeOperationsViewModel),
            new NavigationService(_navigationStore, MakeInvalidOperationView));
    }

    private DenominationSelectionViewModel MakeDenominationSelectionViewModel()
    {
        return new DenominationSelectionViewModel(_atmController,
            new NavigationService(_navigationStore, MakeOperationsViewModel),
            new NavigationService(_navigationStore, MakeWithdrawViewModel),
            new NavigationService(_navigationStore, MakeInvalidOperationView));
    }

    private WithdrawViewModel MakeWithdrawViewModel()
    {
        return new WithdrawViewModel(_atmController,
            new NavigationService(_navigationStore, MakeOperationsViewModel),
            new NavigationService(_navigationStore, MakeDenominationSelectionViewModel),
            new NavigationService(_navigationStore, MakeInvalidOperationView));
    }
}