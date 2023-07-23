﻿using System;
using System.Windows.Input;

namespace TestTask.Commands;

public abstract class CommandBase : ICommand
{
    public virtual bool CanExecute(object? parameter) => true;

    public abstract void Execute(object? parameter);

    public event EventHandler? CanExecuteChanged;

    protected void OnCanExecutedChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}