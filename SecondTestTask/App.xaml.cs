using System.Windows;
using SecondTestTask.Models.Atm;
using SecondTestTask.Models.Services;
using SecondTestTask.Navigation;
using SecondTestTask.Services;
using SecondTestTask.ViewModels;
using SecondTestTask.Views;

namespace SecondTestTask;

public partial class App : Application
{
    private readonly AtmController m_atmController;
    private readonly NavigationStore m_navigationStore;

    public App()
    {
        m_atmController = new AtmController(new AccountService());
        m_navigationStore = new NavigationStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        m_navigationStore.CurrentViewModel = MakeLoginViewModel();
        MainWindow = new MainWindow
        {
            DataContext = new MainWindowViewModel(m_navigationStore)
        };
        MainWindow.Show();

        base.OnStartup(e);
    }

    private OperationsViewModel MakeOperationsViewModel()
    {
        return new OperationsViewModel(m_atmController,
            new NavigationService(m_navigationStore, MakeLoginViewModel),
            new NavigationService(m_navigationStore, MakeBalanceViewModel),
            new NavigationService(m_navigationStore, MakeDepositViewModel),
            new NavigationService(m_navigationStore, MakeWithdrawViewModel));
    }

    private LoginViewModel MakeLoginViewModel()
    {
        return new LoginViewModel(m_atmController,
            new NavigationService(m_navigationStore, MakeOperationsViewModel),
            new NavigationService(m_navigationStore, MakeInvalidOperationView));
    }

    private InvalidOperationViewModel MakeInvalidOperationView()
    {
        return new InvalidOperationViewModel(new NavigationService(m_navigationStore, MakeLoginViewModel));
    }

    private BalanceViewModel MakeBalanceViewModel()
    {
        return new BalanceViewModel(m_atmController,
            new NavigationService(m_navigationStore, MakeOperationsViewModel));
    }

    private DepositViewModel MakeDepositViewModel()
    {
        return new DepositViewModel(m_atmController,
            new NavigationService(m_navigationStore, MakeOperationsViewModel),
            new NavigationService(m_navigationStore, MakeInvalidOperationView));
    }

    private WithdrawViewModel MakeWithdrawViewModel()
    {
        return new WithdrawViewModel(m_atmController,
            new NavigationService(m_navigationStore, MakeOperationsViewModel),
            new NavigationService(m_navigationStore, MakeInvalidOperationView));
    }
}