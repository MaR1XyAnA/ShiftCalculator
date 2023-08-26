///<!--
/// Страница предстовляет из себя специальный калькулятор с журналом.
/// Пользователю необходимо ввести определённые данные, после чего они попадут в специальный класс, где произойдёт счёт
///     и класс выдаст значения, которые получит пользователь. 
/// После значения записываются в журнал.
/// Журнал - это обычный txt файл, куда определённым образом просто записываются данные и всё.
///-->

using ShiftCalculator.AppDataFolder.ClassFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ShiftCalculator.PerformanceFolder.PageFolder
{
    public partial class ProfitPage : Page
    {
        string filePath = "HistoryDocument.txt";

        public ProfitPage()
        {
            InitializeComponent();
            Event_OutputData();
            DateDatePickerTextBox.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        #region _TextChanged
        private void TextHint_TextChanged(object sender, TextChangedEventArgs e) ///Показать\Спрятать текстовую подсказку
        {
            TextBox textBox = (TextBox)sender;
            TextBlock hintBlock = null;

            switch (textBox.Name)
            {
                case "DateDatePickerTextBox":
                    hintBlock = HintDateTextBlock;
                    break;
                case "TotalAmountTextBox":
                    hintBlock = HintTotalAmountTextBlock;
                    break;
                case "CashlessPaymentTextBox":
                    hintBlock = HintCashlessPaymentTextBlock;
                    break;
                case "PreviousCashBalanceTextBox":
                    hintBlock = HintPreviousCashBalanceTextBlock;
                    break;
                case "BanckTextBox":
                    hintBlock = HintBanckTextBlock;
                    break;
            }

            if (textBox.Text.Length == 0)
            {
                hintBlock.Visibility = Visibility.Visible;
            }
            else
            {
                hintBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void HintCashBalanceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HintSweepButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        #region _Click
        private void SetToDayDate_Click(object sender, RoutedEventArgs e) /// В специальное поле выводит дату "ToDay"
        {
            DateDatePickerTextBox.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e) /// Отправка в класс данных и их получение и вывод
        {
            CalculateClass calculateClass = new CalculateClass();

            CalculateClass.BalanceResult result = calculateClass.CalculateBalances(
                Convert.ToDouble(TotalAmountTextBox.Text), 
                Convert.ToDouble(PreviousCashBalanceTextBox.Text), 
                Convert.ToDouble(CashlessPaymentTextBox.Text), 
                Convert.ToDouble(BanckTextBox.Text));

            CashBalanceTextBox.Text = result.CashBalance.ToString();
            TotalForTheDayTextBox.Text = result.TotalForTheDay.ToString();

            Event_RecordingHistory();
            Event_OutputData();
        }

        private void HintButton_Click(object sender, RoutedEventArgs e) /// Подсказки по нажатию на кнопку
        {
            string textMessage = "";
            string topRow = "";

            Button hintButton = (Button)sender;

            switch (hintButton.Name)
            {
                case "HintDateButton":
                    textMessage = 
                        "Параметр, который нужен для ввода даты того числа, когда создаётся подсчёт\n" +
                        "Если приследуется цели просто посчитать данные, то без ввода даты запись не будет создана";
                    topRow = "Информация про дату";
                    break;

                case "HintTotalAmountButton":
                    textMessage = 
                        "Общая сумма, которая вышла за смену (на чеке напиана общая сумма, которую нужно вбить в данное поле)";
                    topRow = "Общая сумма";
                    break;

                case "HintCashlessPaymentButton":
                    textMessage = 
                        "Сумма, которая вышла за смену по оплате картой (на чеке напиана сумма, которую нужно вбить в данное поле)";
                    topRow = "Безналичный расчёт";
                    break;

                case "HintPreviousCashBalanceButton":
                    textMessage = 
                        "Сумма, которая осталась в кассе с прошлой смены";
                    topRow = "Кассовый остаток";
                    break;

                case "HintBanckButton":
                    textMessage = 
                        "Сумма, которая убирается в пакет (банк)\n" +
                        "0, 500, 1000, 1500, 2000, 2500 и т.д.";
                    topRow = "Банк";
                    break;
            }

            MessageBoxClass.HintMessageBox_MBC(textMessage, topRow);
        }
        #endregion
        #region Event_
        private async void Event_RecordingHistory() /// Запись истории подсчёта
        {
            string matrixRecording =
                        $"{"[Дата]",-20}{DateTime.Now.ToString("dd/MM/yyyy")}\n" +
                        $"{"[Время]",-20}{DateTime.Now.ToString("HH:mm:ss")}\n" +
                        $"{"[Общая сумма]",-20}{TotalAmountTextBox.Text}\n" +
                        $"{"[Банк]",-20}{BanckTextBox.Text}\n" +
                        $"{"[Кассовый остаток]",-20}{CashBalanceTextBox.Text}\n" +
                        $"{"[Безналичный]",-20}{CashlessPaymentTextBox.Text}\n" +
                        $"{"[Общая за день]",-20}{TotalForTheDayTextBox.Text}\n";

            using (StreamWriter recordingHistory = new StreamWriter(filePath, true))
            {
                await recordingHistory.WriteLineAsync(matrixRecording);
            }
        }

        private async void Event_OutputData() /// Выводит историю подсчёта
        {
            using (StreamReader outputData = new StreamReader(filePath))
            {
                List<HistoryClass> historyEntries = new List<HistoryClass>();

                string line;
                HistoryClass currentEntry = new HistoryClass();

                while ((line = await outputData.ReadLineAsync()) != null)
                {
                    if (line.Contains("[Дата]"))
                    {
                        currentEntry.Date_HC = line.Split('[', ']')[2].Trim();
                    }
                    else if (line.Contains("[Время]"))
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
                        currentEntry = new HistoryClass();
                    }
                }

                HistoryDataGrid.ItemsSource = historyEntries;
            }
        }
        #endregion
    }
}
