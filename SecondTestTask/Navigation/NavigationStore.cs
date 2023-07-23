using System;
using SecondTestTask.ViewModels;

namespace SecondTestTask.Navigation;

public class NavigationStore
{
    public ViewModelBase CurrentViewModel
    {
        get => m_currentViewModel;
        set
        {
            m_currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    public event Action CurrentViewModelChanged;

    private ViewModelBase m_currentViewModel;

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }
}