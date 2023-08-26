using ShiftCalculator.AppDataFolder.ClassFolder;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace ShiftCalculator
{
    public partial class App : Application 
    {
        public App()
        {
            DispatcherUnhandledException += Application_DispatcherUnhandledException;
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs exception)
        {
            MessageBoxClass.ExceptionMessageBox_MBC(textMessage: 
                $"Возникло необработанное исключение: {exception.Exception.Message}\n");
            exception.Handled = true;
        }
    }
}
