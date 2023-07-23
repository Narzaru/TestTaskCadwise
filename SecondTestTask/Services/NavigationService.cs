using System;
using SecondTestTask.Navigation;
using SecondTestTask.ViewModels;

namespace SecondTestTask.Services;

public class NavigationService
{
    private readonly NavigationStore m_navigationStore;
    private readonly Func<ViewModelBase> m_createViewModel;

    public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
    {
        m_createViewModel = createViewModel;
        m_navigationStore = navigationStore;
    }

    public void Navigate()
    {
        m_navigationStore.CurrentViewModel = m_createViewModel();
    }
}