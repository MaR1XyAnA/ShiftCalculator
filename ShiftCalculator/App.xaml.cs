using ShiftCalculator.AppDataFolder.ClassFolder;
using ShiftCalculator.Properties;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace ShiftCalculator
{
    public partial class App : Application 
    {
        public App()
        {
            DispatcherUnhandledException += Application_DispatcherUnhandledException;
            //Event_CheckingForAvailability();
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs exception)
        {
            MessageBoxClass.ExceptionMessageBox_MBC(textMessage:
                $"Возникло необработанное исключение: {exception.Exception.Message}\n");
            exception.Handled = true;
        }

        private void Event_CheckingForAvailability() /// Проверка на существующие файлы перед началом работы приложения
        {
            if (!File.Exists(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup))
            {
                MessageBox.Show("0");
                return;
            }
            if (!File.Exists(Settings.Default.ThePathToTheFileToSaveTheShiftCountingHistory))
            {
                MessageBox.Show("1");
                return;
            }
        }
    }
}
