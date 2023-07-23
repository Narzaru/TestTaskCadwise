using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestTask.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}