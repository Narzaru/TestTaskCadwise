using System.Windows.Input;
using Atm.WpfUi.Commands;
using Atm.WpfUi.Services;

namespace Atm.WpfUi.ViewModels;

public class InvalidOperationViewModel : ViewModelBase
{
    public InvalidOperationViewModel(NavigationService toLogin)
    {
        BackCommand = new InvalidOperationViewAfterInvalidOkCommand(toLogin);
    }

    public ICommand BackCommand { get; set; }
}