using System.Windows.Input;
using SecondTestTask.Commands;
using SecondTestTask.Services;

namespace SecondTestTask.ViewModels;

public class InvalidOperationViewModel : ViewModelBase
{
    public InvalidOperationViewModel(NavigationService toLogin)
    {
        BackCommand = new AfterInvalidOkCommand(toLogin);
    }

    public ICommand BackCommand { get; set; }
}