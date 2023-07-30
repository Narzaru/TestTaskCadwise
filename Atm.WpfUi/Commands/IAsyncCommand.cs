using System.Threading.Tasks;
using System.Windows.Input;

namespace Atm.WpfUi.Commands;

public interface IAsyncCommand : ICommand
{
    Task ExecuteAsync(object? parameter);
}