///<!--
/// Страница предстовляет из себя специальный калькулятор с журналом.
/// Пользователю необходимо ввести определённые данные, после чего они попадут в специальный класс, где произойдёт счёт
///     и класс выдаст значения, которые получит пользователь. 
/// После значения записываются в журнал.
/// Журнал - это обычный txt файл, куда определённым образом просто записываются данные и всё.
///-->

using ShiftCalculator.AppDataFolder.ClassFolder;
using ShiftCalculator.PerformanceFolder.WindowFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ShiftCalculator.PerformanceFolder.PageFolder
{
    public partial class ProfitPage : Page
    {
        public static List<HistoryClass> selectedItemList = new List<HistoryClass>();

        string filePath = "HistoryDocument.txt";
        string isNullOrWhiteSpaceTextBox;

        DispatcherTimer dispatcherTimer;

        public ProfitPage()
        {
            try
            {
                InitializeComponent();
                Event_OutputData();
                Event_SettingsDispatcherTimer();
                Event_ToggleDateTimeControls(true);

                HistoryDataGrid.Items.SortDescriptions.Add(new SortDescription("LineNumber_HC", ListSortDirection.Descending));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        #region _TextChanged
        private void TextHint_TextChanged(object sender, TextChangedEventArgs e) ///Показать\Спрятать текстовую подсказку
        {
            TextBox textBox = (TextBox)sender;
            TextBlock hintTextBlock = null;

            switch (textBox.Name)
            {
                case "DateTextBox":
                    hintTextBlock = HintTimeTextBlock;
                    break;
                case "TimeTextBox":
                    hintTextBlock = HintDateTextBlock;
                    break;
                case "TotalAmountTextBox":
                    hintTextBlock = HintTotalAmountTextBlock;
                    break;
                case "CashlessPaymentTextBox":
                    hintTextBlock = HintCashlessPaymentTextBlock;
                    break;
                case "PreviousCashBalanceTextBox":
                    hintTextBlock = HintPreviousCashBalanceTextBlock;
                    break;
                case "BanckTextBox":
                    hintTextBlock = HintBanckTextBlock;
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
        #region _Click
        private void SetToDayDateTime_Click(object sender, RoutedEventArgs e)
        {
            if (SetToDayDateTime.IsChecked == true)
            {
                Event_ToggleDateTimeControls(true);
            }
            else
            {
                Event_ToggleDateTimeControls(false);
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e) /// Работа кнопки
        {
            isNullOrWhiteSpaceTextBox = "";

            Event_IsNullOrWhiteSpaceTextBox();

            if (isNullOrWhiteSpaceTextBox != "")
            {
                MessageBoxClass.ErrorMessageBox_MBC(textMessage: isNullOrWhiteSpaceTextBox, topRow: "Текстовое поле не должно быть пустым");
            }
            else
            {
                Event_PerformCountingOperation();
            }
        }

        private void HintButton_Click(object sender, RoutedEventArgs e) /// Подсказки по нажатию на кнопку
        {
            string textHint = "";
            string nameHint = "";

            Button hintButton = (Button)sender;

            switch (hintButton.Name)
            {
                case "HintDateButton":
                    textHint =
                        "Этот параметр предназначен для указания даты, когда была создана запись о подсчете данных.\n" +
                        "Если ваша цель просто подсчитать информацию, то без внесения даты записи она не будет создана.";
                    nameHint = "Информация о дате";
                    break;

                case "HintTotalAmountButton":
                    textHint =
                        "Это поле предназначено для ввода общей суммы, которая была получена за смену.\n" +
                        "На чеке указывается итоговая сумма, которую необходимо внести в данное поле.";
                    nameHint = "Информация о общей сумме";
                    break;

                case "HintCashlessPaymentButton":
                    textHint =
                        "Это поле предназначено для ввода суммы, полученной за смену в результате оплаты картой.\n" +
                        "На чеке указана сумма, которую необходимо внести в данное поле.";
                    nameHint = "Информация о безналичном расчете";
                    break;

                case "HintPreviousCashBalanceButton":
                    textHint =
                        "Это поле отражает сумму, которая осталась в кассе с предыдущей смены.\n" +
                        "Данную информацию можно посмотреть в журнале приложения, или в записывающем журнале";
                    nameHint = "Инфомация о кассовом остатке";
                    break;

                case "HintBanckButton":
                    textHint =
                        "Это поле предназначено для указания суммы, которая будет убрана в банк (пакет).\n" +
                        "Обычно используются стандартные номинации, такие как 0, 500, 1000, 1500, 2000, 2500 и так далее.";
                    nameHint = "Информация о банке";
                    break;
            }

            MessageBoxClass.HintMessageBox_MBC(textMessage: textHint, topRow: nameHint);
        }

        private void DeleteAnEntryButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void ResetSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryDataGrid.SelectedItem = null;
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        #region Event_
        private void Event_ToggleDateTimeControls(bool enable)
        {
            DateTextBox.IsEnabled = !enable;
            TimeTextBox.IsEnabled = !enable;

            if (enable) 
            { 
                dispatcherTimer.Start(); 
            }
            else
            { 
                dispatcherTimer.Stop(); 
            }
        }
        private void Event_SettingsDispatcherTimer() /// Настройки таймера
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Event_DispatcherTimer);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1);
        }

        private void Event_DispatcherTimer(object sender, EventArgs e) /// Событие для таймера
        {
            DateTime toDaytime = DateTime.Now;

            DateTextBox.Text = toDaytime.ToString("dd.MM.yyyy");
            TimeTextBox.Text = toDaytime.ToString("HH:mm:ss");
        }

        private void Event_IsNullOrWhiteSpaceTextBox() /// Проверка текстовых полей на пустоту
        {
            if (string.IsNullOrWhiteSpace(TotalAmountTextBox.Text))
            {
                isNullOrWhiteSpaceTextBox += "● Текстовое поле \"Общая сумма\" не должно быть пустым.\n\n";
            }

            if (string.IsNullOrWhiteSpace(CashlessPaymentTextBox.Text))
            {
                isNullOrWhiteSpaceTextBox += "● Текстовое поле \"Безналичный расчёт\" не должно быть пустым.\n\n";
            }

            if (string.IsNullOrWhiteSpace(PreviousCashBalanceTextBox.Text))
            {
                isNullOrWhiteSpaceTextBox += "● Текстовое поле \"Предыдуший кассовый остаток\" не должно быть пустым.\n\n";
            }

            if (string.IsNullOrWhiteSpace(BanckTextBox.Text))
            {
                isNullOrWhiteSpaceTextBox += "● Текстовое поле \"В банке\" не должно быть пустым.";
            }
        }

        private void Event_CheckingBeforeRecording()
        {
            if (SaveToggleButton.IsChecked == true)
            {
                string dateFormat = "dd.MM.yyyy";
                DateTime dateResult;
                TimeSpan timeResult;

                if (SetToDayDateTime.IsChecked == false)
                {
                    if (DateTime.TryParseExact(DateTextBox.Text, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateResult))
                    {
                        if (TimeSpan.TryParse(TimeTextBox.Text, out timeResult))
                        {
                            Event_RecordingHistory();
                            return;
                        }
                    }
                }
                else
                {
                    Event_RecordingHistory();
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private async void Event_RecordingHistory() /// Запись истории подсчёта
        {
            string matrixRecording =
                $"{"[Дата]",-20}{DateTextBox.Text}\n" +
                $"{"[Время]",-20}{TimeTextBox.Text}\n" +
                $"{"[Общая сумма]",-20}{TotalAmountTextBox.Text}\n" +
                $"{"[Банк]",-20}{BanckTextBox.Text}\n" +
                $"{"[Кассовый остаток]",-20}{CashBalanceTextBlock.Text}\n" +
                $"{"[Безналичный]",-20}{CashlessPaymentTextBox.Text}\n" +
                $"{"[Общая за день]",-20}{TotalForTheDayTextBlock.Text}\n";

            using (StreamWriter recordingHistory = new StreamWriter(filePath, true))
            {
                await recordingHistory.WriteLineAsync(matrixRecording);
                HistoryDataGrid.Items.SortDescriptions.Add(new SortDescription("LineNumber_HC", ListSortDirection.Descending));
            }
        }

        private async void Event_OutputData()
        {
            using (StreamReader outputData = new StreamReader(filePath))
            {
                List<HistoryClass> historyEntries = new List<HistoryClass>();

                string line;
                HistoryClass currentEntry = null; // Начнем с null, чтобы первую запись создать

                int rowNumber = 1; // Начнем с 1 для нумерации строк

                while ((line = await outputData.ReadLineAsync()) != null)
                {
                    if (line.Contains("[Дата]"))
                    {
                        currentEntry = new HistoryClass(); // Создаем новую запись для новой секции
                        currentEntry.LineNumber_HC = rowNumber++; // Устанавливаем номер строки и затем увеличиваем его на 1

                        currentEntry.Date_HC = line.Split('[', ']')[2].Trim();
                    }
                    else if (currentEntry != null) // Проверяем, что запись была создана
                    {
                        if (line.Contains("[Время]"))
                        {
                            currentEntry.Time_HC = line.Split('[', ']')[2].Trim();
                        }
                        else if (line.Contains("[Общая сумма]"))
                        {
                            double.TryParse(line.Split('[', ']')[2].Trim(), out double totalAmountValue);
                            currentEntry.TotalAmount_HC = totalAmountValue;
                        }
                        else if (line.Contains("[Банк]"))
                        {
                            double.TryParse(line.Split('[', ']')[2].Trim(), out double bankValue);
                            currentEntry.Bank_HC = bankValue;
                        }
                        else if (line.Contains("[Кассовый остаток]"))
                        {
                            double.TryParse(line.Split('[', ']')[2].Trim(), out double cashBalanceValue);
                            currentEntry.CashBalance_HC = cashBalanceValue;
                        }
                        else if (line.Contains("[Безналичный]"))
                        {
                            double.TryParse(line.Split('[', ']')[2].Trim(), out double cashlessValue);
                            currentEntry.Cashless_HC = cashlessValue;
                        }
                        else if (line.Contains("[Общая за день]"))
                        {
                            double.TryParse(line.Split('[', ']')[2].Trim(), out double totalForTheDayValue);
                            currentEntry.TotalForTheDay_HC = totalForTheDayValue;
                        }
                        else if (string.IsNullOrWhiteSpace(line))
                        {
                            historyEntries.Add(currentEntry);
                        }
                    }
                }

                HistoryDataGrid.ItemsSource = historyEntries;
            }
        }

        private void Event_PerformCountingOperation()
        {
            CalculateClass calculateClass = new CalculateClass();

            CalculateClass.CashWithdrawal calculationResult = calculateClass._CashWithdrawal(
                Convert.ToDouble(TotalAmountTextBox.Text.Replace(".", ",")),
                Convert.ToDouble(PreviousCashBalanceTextBox.Text.Replace(".", ",")),
                Convert.ToDouble(CashlessPaymentTextBox.Text.Replace(".", ",")),
                Convert.ToDouble(BanckTextBox.Text.Replace(".", ",")));

            CashBalanceTextBlock.Text = calculationResult.CashBalance_CW.ToString();
            TotalForTheDayTextBlock.Text = calculationResult.TotalForTheDay_CW.ToString();

            Event_CheckingBeforeRecording();
            Event_OutputData();
        }

        private void Event_EnteringOnlyNumbersTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex DivisionCodeRegex = new Regex("[^0-9.,]");
            e.Handled = DivisionCodeRegex.IsMatch(e.Text);
        }
        #endregion

        private void HistoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var selectedItem in HistoryDataGrid.SelectedItems)
            {
                if (selectedItem is HistoryClass historyItem)
                {
                    selectedItemList.Add(historyItem);
                }
            }

            if (HistoryDataGrid.SelectedItem != null)
            {
                DeleteAnEntryButton.IsEnabled = true;
                ResetSelectionButton.IsEnabled = true;
                CopyButton.IsEnabled = true;
                DownloadButton.IsEnabled = true;
            }
            else
            {
                DeleteAnEntryButton.IsEnabled = false;
                ResetSelectionButton.IsEnabled = false;
                CopyButton.IsEnabled = false;
                DownloadButton.IsEnabled = false;
            }
        }

    }
}
