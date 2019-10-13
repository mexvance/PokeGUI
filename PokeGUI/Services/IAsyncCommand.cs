﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokeGUI.Services
{
    public interface IAsyncCommand : IAsyncCommand<object>
    {
    }

    public interface IAsyncCommand<in T>
    {
        Task ExecuteAsync(T obj);
        bool CanExecute(object obj);
        ICommand Command { get; }
    }
}
