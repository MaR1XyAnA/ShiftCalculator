using ShiftCalculator.AppDataFolder.ClassFolder;
using ShiftCalculator.PerformanceFolder.PageFolder;
using System;
using System.Windows;
using System.Windows.Input;

namespace ShiftCalculator.PerformanceFolder.WindowFolder
{
    public partial class MainWindow : Window
    {
        string mainNameWindow = "MainWindow:\n\n";

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Event_WorkWithFrame();
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие MainWindow в {mainNameWindow}" + $"{ex.Message}");
            }
        }
        #region Event_
        private void Event_WorkWithFrame()
        {
            try
            {
                FrameNavigationClass.mainSpace_FNC = BodyFrame;
                FrameNavigationClass.mainSpace_FNC.Navigate(new MainPage());
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие Event_WorkWithFrame в {mainNameWindow}" + $"{ex.Message}");
            }
        }
        #endregion
        #region Управление окном
        private void TopShelGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left) { this.DragMove(); }
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие TopShelGrid_MouseDown в {mainNameWindow}" + $"{ex.Message}");
            }
        }

        private void RollupWindowButton_Click(object sender, RoutedEventArgs e)
        {
            try { WindowState = WindowState.Minimized; }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие RollupWindowButton_Click в {mainNameWindow}" + $"{ex.Message}");
            }
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            try { Application.Current.Shutdown(); }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие CloseWindowButton_Click в {mainNameWindow}" + $"{ex.Message}");
            }
        }
        #endregion
    }
}
