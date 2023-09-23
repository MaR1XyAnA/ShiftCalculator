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
        public ProductPage()
        {
            InitializeComponent();
        }
        #region Текстовые подсказки
        private void ExtraChargeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void QuantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CostTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ResultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion
        #region _Click
        private void HintButton_Click(object sender, RoutedEventArgs e) /// Подсказки по нажатию на кнопку
        {
            string textHint = "";
            string nameHint = "";

            Button hintButton = (Button)sender;

            switch (hintButton.Name)
            {
                case "HintExtraChargeButton":
                    textHint = 
                        "Этот параметр отвечает за то, какая наценка будет у товара (%).\n" +
                        "К примеру, товар стоит 100 рублей, а наценка 10%, значит товар будет стоить 110 рублей";
                    nameHint = "Подсказка про наценку";
                    break;

                case "HintQuantityButton":
                    textHint = "Количесто товара, который привезли (специальная выделенная графа в накладной)";
                    nameHint = "Подсказка по количеству";
                    break;

                case "HintCostButton":
                    textHint = "Цена товара, который привези (специальная выделенная графа в накладной)";
                    nameHint = "Подсказка про цену";
                    break;

                case "HintResultButton":
                    textHint = "Конечная стоимость товара за шт с учётом наценки";
                    nameHint = "Подсказка по результату";
                    break;
            }

            MessageBoxClass.HintMessageBox_MBC(textMessage: textHint, topRow: nameHint);
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            CalculateClass calculateClass = new CalculateClass();

            CalculateClass.CalculatingMargins result = calculateClass._CalculatingMargins(
                Convert.ToDouble(ExtraChargeTextBox.Text),
                Convert.ToDouble(QuantityTextBox.Text),
                Convert.ToDouble(CostTextBox.Text));

            ResultTextBox.Text = result.ResultCalculations_CM.ToString();
        }
        #endregion
        #region _TextChanged
        private void TextHint_TextChanged(object sender, TextChangedEventArgs e) ///Показать\Спрятать текстовую подсказку
        {
            TextBox textBox = (TextBox)sender;
            TextBlock hintTextBlock = null;

            switch (textBox.Name)
            {
                case "ExtraChargeTextBox":
                    hintTextBlock = HintExtraChargeTextBlock;
                    break;
                case "QuantityTextBox":
                    hintTextBlock = HintQuantityTextBlock;
                    break;
                case "CostTextBox":
                    hintTextBlock = HintCostTextBlock;
                    break;
            }

            if (textBox.Text.Length == 0)
            {
                hintTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                hintTextBlock.Visibility = Visibility.Collapsed;
            }
        }
        #endregion
        #region Event_

        #endregion
    }
}
