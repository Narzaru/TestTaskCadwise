using SecondTestTask.Navigation;

namespace SecondTestTask.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private NavigationStore m_navigationStore;

    public MainWindowViewModel(NavigationStore navigationStore)
    {
        m_navigationStore = navigationStore;
        m_navigationStore.CurrentViewModelChanged += () => { OnPropertyChanged(nameof(CurrentViewModel)); };
    }

    public ViewModelBase CurrentViewModel => m_navigationStore.CurrentViewModel;
}