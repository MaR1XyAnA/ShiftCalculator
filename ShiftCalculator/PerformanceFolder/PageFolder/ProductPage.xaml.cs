///<!--
/// Страница, для подсчёта накенки товара
///-->

using Newtonsoft.Json;
using ShiftCalculator.AppDataFolder.ClassFolder;
using ShiftCalculator.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            CalculateClass calculateClass = new CalculateClass();

            CalculateClass.CalculateTheCostOfTheDoods_CC calculationResult = calculateClass._CalculateTheCostOfTheDoods(
                Convert.ToDouble(ExtraChargeTextBox.Text.Replace(".", ",")),
                Convert.ToDouble(QuantityTextBox.Text),
                Convert.ToDouble(CostTextBox.Text.Replace(".", ",")));

            CalculationResultTextBlock.Text = calculationResult.ResultCalculations_CTCOTD.ToString();
            RoundedPriceTextBlock.Text = Math.Ceiling(calculationResult.ResultCalculations_CTCOTD).ToString();

            CalculationResultTextBlock.Foreground = calculationResult.ResultCalculations_CTCOTD <= 0
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3D71"))
                : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E8BE0"));

            RoundedPriceTextBlock.Foreground = Math.Ceiling(calculationResult.ResultCalculations_CTCOTD) <= 0
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3D71"))
                : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E8BE0"));

            Event_RecordingHistory();
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
        private void Event_RecordingHistory() /// Запись истории подсчёта
        {
            List<HistoryClass.CalculationsOnTheCostOfGoods_HC> recordingNewData;

            var matrixDataRecording = new HistoryClass.CalculationsOnTheCostOfGoods_HC()
            {
                ExtraCharge_COTCOGC = Convert.ToDouble(ExtraChargeTextBox.Text),
                Quantity_COTCOGC = Convert.ToDouble(QuantityTextBox.Text),
                Costs_COTCOGC = Convert.ToDouble(CostTextBox.Text),
                CalculationResult_COTCOGC = Convert.ToDouble(CalculationResultTextBlock.Text),
                Result_COTCOGC = Convert.ToDouble(RoundedPriceTextBlock.Text)
            };

            // Проверяем, существует ли файл
            if (File.Exists(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup))
            {
                // Если файл существует, загружаем его содержимое
                recordingNewData = JsonConvert.DeserializeObject<List<HistoryClass.CalculationsOnTheCostOfGoods_HC>>(
                    File.ReadAllText(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup));
            }
            else
            {
                // Если файл не существует или содержит некорректный JSON, создаем новый список
                recordingNewData = new List<HistoryClass.CalculationsOnTheCostOfGoods_HC>();
            }

            recordingNewData.Add(matrixDataRecording);

            // Сохраняем список в файл
            File.WriteAllText(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup,
                JsonConvert.SerializeObject(recordingNewData, Formatting.Indented));
        }

        private void Event_EnterNumbersAndCommasTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex DivisionCodeRegex = new Regex("[^0-9.,]");
            e.Handled = DivisionCodeRegex.IsMatch(e.Text);
        }

        private void Event_EnterNumbersTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex DivisionCodeRegex = new Regex("[^0-9]");
            e.Handled = DivisionCodeRegex.IsMatch(e.Text);
        }
        #endregion
    }
}
