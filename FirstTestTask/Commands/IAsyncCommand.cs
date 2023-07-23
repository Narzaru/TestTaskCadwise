using System.Threading.Tasks;
using System.Windows.Input;

namespace TestTask.Commands;

public interface IAsyncCommand : ICommand
{
    Task ExecuteAsync(object? parameter);
}