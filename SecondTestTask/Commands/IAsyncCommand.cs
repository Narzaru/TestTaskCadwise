using System.Threading.Tasks;
using System.Windows.Input;

namespace SecondTestTask.Commands;

public interface IAsyncCommand : ICommand
{
    Task ExecuteAsync(object? parameter);
}