using System;
using Atm.WpfUi.ViewModels;

namespace Atm.WpfUi.Navigation;

public class NavigationStore
{
    private ViewModelBase _currentViewModel = null!;

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    public event Action? CurrentViewModelChanged;

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }

    public MessageBus MessageBus { get; set; } = new();
}