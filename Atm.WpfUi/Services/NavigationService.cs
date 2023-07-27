using System;
using SecondTestTask.Navigation;
using SecondTestTask.ViewModels;

namespace SecondTestTask.Services;

public class NavigationService
{
    private readonly Func<ViewModelBase> _createViewModel;
    private readonly NavigationStore _navigationStore;

    public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
    {
        _createViewModel = createViewModel;
        _navigationStore = navigationStore;
    }

    public void Navigate()
    {
        _navigationStore.CurrentViewModel = _createViewModel();
    }
}