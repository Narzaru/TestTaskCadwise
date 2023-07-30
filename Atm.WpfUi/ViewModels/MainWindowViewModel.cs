using Atm.WpfUi.Navigation;

namespace Atm.WpfUi.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private NavigationStore _navigationStore;

    public MainWindowViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += () => { OnPropertyChanged(nameof(CurrentViewModel)); };
    }

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
}