using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DBsuppliers.VMTools
{
    public class CommandVM : ICommand
    {
        // пример самой простой реализации команды:
        // команда доступна для вызова всегда
        // команда не работает с параметром команды
        // команда содержит ссылку на лямбду, которая выполняется при выполнении команды


        Action action;

        public CommandVM(Action action)
        {
            this.action = action;
        }

        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            action();
        }
    }
}
