using System;
using SecondTestTask.ViewModels;

namespace SecondTestTask.Navigation;

public class NavigationStore
{
    private ViewModelBase m_currentViewModel;

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

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }
}