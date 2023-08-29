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
            TextBlock hintTextBlock = null;

            switch (textBox.Name)
            {
                case "DateDatePickerTextBox":
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
        private void SetToDayDate_Click(object sender, RoutedEventArgs e) /// В специальное поле выводит дату "ToDay"
        {
            DateDatePickerTextBox.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e) /// Отправка в класс данных и их получение и вывод
        {
            CalculateClass calculateClass = new CalculateClass();

            CalculateClass.CashWithdrawal result = calculateClass._CashWithdrawal(
                Convert.ToDouble(TotalAmountTextBox.Text), 
                Convert.ToDouble(PreviousCashBalanceTextBox.Text), 
                Convert.ToDouble(CashlessPaymentTextBox.Text), 
                Convert.ToDouble(BanckTextBox.Text));

            CashBalanceTextBox.Text = result.CashBalance_CW.ToString();
            TotalForTheDayTextBox.Text = result.TotalForTheDay_CW.ToString();

            Event_RecordingHistory();
            Event_OutputData();
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
                        "Параметр, который нужен для ввода даты того числа, когда создаётся подсчёт\n" +
                        "Если приследуется цели просто посчитать данные, то без ввода даты запись не будет создана";
                    nameHint = "Информация про дату";
                    break;

                case "HintTotalAmountButton":
                    textHint = "Общая сумма, которая вышла за смену (на чеке напиана общая сумма, которую нужно вбить в данное поле)";
                    nameHint = "Общая сумма";
                    break;

                case "HintCashlessPaymentButton":
                    textHint = "Сумма, которая вышла за смену по оплате картой (на чеке напиана сумма, которую нужно вбить в данное поле)";
                    nameHint = "Безналичный расчёт";
                    break;

                case "HintPreviousCashBalanceButton":
                    textHint = "Сумма, которая осталась в кассе с прошлой смены";
                    nameHint = "Кассовый остаток";
                    break;

                case "HintBanckButton":
                    textHint = 
                        "Сумма, которая убирается в пакет (банк)\n" +
                        "0, 500, 1000, 1500, 2000, 2500 и т.д.";
                    nameHint = "Банк";
                    break;
            }

            MessageBoxClass.HintMessageBox_MBC(textMessage:textHint, topRow:nameHint);
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
