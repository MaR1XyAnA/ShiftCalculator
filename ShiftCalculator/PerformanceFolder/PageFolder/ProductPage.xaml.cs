///<!--
/// Страница, для подсчёта накенки товара
///-->

using ShiftCalculator.AppDataFolder.ClassFolder;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ShiftCalculator.PerformanceFolder.PageFolder
{
    public partial class ProductPage : Page
    {
        string mainNamePage = "ProductPage:\n\n";

        public ProductPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие ProductPage в {mainNamePage}" + $"{ex.Message}");
            }
        }
        #region Текстовые подсказки
        private void ExtraChargeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие ExtraChargeTextBox_TextChanged в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void QuantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие QuantityTextBox_TextChanged в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void CostTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие CostTextBox_TextChanged в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void ResultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие ResultTextBox_TextChanged в {mainNamePage}" + $"{ex.Message}");
            }
        }
        #endregion
        #region _Click
        private void HintExtraChargeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие HintExtraChargeButton_Click в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void HintQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие HintQuantityButton_Click в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void HintCostButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие HintCostButton_Click в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void HintResultButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие HintResultButton_Click в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие CalculateButton_Click в {mainNamePage}" + $"{ex.Message}");
            }
        }
        #endregion
        #region Event_

        #endregion
    }
}
