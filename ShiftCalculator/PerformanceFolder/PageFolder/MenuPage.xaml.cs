using ShiftCalculator.AppDataFolder.ClassFolder;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ShiftCalculator.PerformanceFolder.PageFolder
{
    public partial class MenuPage : Page
    {
        string mainNamePage = "MenuPage:\n\n";

        public MenuPage()
        {
            try
            {
                InitializeComponent();
                Event_StartToggleButton();
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие MenuPage в {mainNamePage}" + $"{ex.Message}");
            }
        }

        #region _Click
        private void ProfitToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Event_StartToggleButton();
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие ProfitToggleButton_Click в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void ProductToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Event_DeactivateToggleButton();
                ProductToggleButton.IsChecked = true;
                FrameNavigationClass.mainSpace_FNC.Navigate(new ProductPage());
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие ProductToggleButton_Click в {mainNamePage}" + $"{ex.Message}");
            }
        }
        #endregion
        #region Event_
        private void Event_DeactivateToggleButton()
        {
            try
            {
                ProductToggleButton.IsChecked = false;
                ProfitToggleButton.IsChecked = false;
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие Event_DeactivateToggleButton в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void Event_StartToggleButton()
        {
            try
            {
                Event_DeactivateToggleButton();
                ProfitToggleButton.IsChecked = true;
                FrameNavigationClass.mainSpace_FNC.Navigate(new ProfitPage());
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие Event_StartToggleButton в {mainNamePage}" + $"{ex.Message}");
            }
        }
        #endregion
    }
}
