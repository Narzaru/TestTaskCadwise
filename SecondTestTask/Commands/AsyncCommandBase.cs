using System;
using System.Threading.Tasks;

namespace SecondTestTask.Commands;

public abstract class AsyncCommandBase : IAsyncCommand
{
    public async void Execute(object? parameter)
    {
        await ExecuteAsync(parameter);
    }

    public abstract Task ExecuteAsync(object? parameter);

    public virtual bool CanExecute(object? parameter)
    {
        return true;
    }

    public event EventHandler? CanExecuteChanged;

    protected void OnCanExecutedChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}