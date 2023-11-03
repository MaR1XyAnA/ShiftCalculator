using ShiftCalculator.AppDataFolder.ClassFolder;
using ShiftCalculator.PerformanceFolder.PageFolder;
using ShiftCalculator.PerformanceFolder.WindowFolder;
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
            MainWindow mainWindow = new MainWindow();

            if (!File.Exists(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup))
            {
                if(MessageBoxClass.MessageBoxActiveButton_MBC(topRow: "Ошибка файла", textMessage: "Отсутствует путь к записи подсчёта прибыли") == MessageBoxResult.OK)
                {
                    mainWindow.Show();
                    FrameNavigationClass.bodySpace_FNC.Navigate(new SettingsPage());
                }
            }
            if (!File.Exists(Settings.Default.ThePathToTheFileToSaveTheShiftCountingHistory))
            {
                if (MessageBoxClass.MessageBoxActiveButton_MBC(topRow: "Ошибка файла", textMessage: "Отсутствует путь к записи подсчёта прибыли") == MessageBoxResult.OK)
                {
                    mainWindow.Show();
                    FrameNavigationClass.bodySpace_FNC.Navigate(new SettingsPage());
                }
            }
        }
    }
}
