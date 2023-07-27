using System.Threading.Tasks;
using System.Windows.Input;

namespace Ui.Commands;

public interface IAsyncCommand : ICommand
{
    Task ExecuteAsync(object? parameter);
}