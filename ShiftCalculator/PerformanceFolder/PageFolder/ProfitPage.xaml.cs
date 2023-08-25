using ShiftCalculator.AppDataFolder.ClassFolder;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ShiftCalculator.PerformanceFolder.PageFolder
{
    public partial class ProfitPage : Page
    {
        string mainNamePage = "ProfitPage:\n\n";
        string filePath = "HistoryDocument.txt";

        public ProfitPage()
        {
            try
            {
                InitializeComponent();
                Event_OutputData();
                DateDatePickerTextBox.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие DateDatePickerTextBox_TextChanged в {mainNamePage}" + $"{ex.Message}");
            }
        }

        #region Текстовые подсказки
        private void DateDatePickerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (DateDatePickerTextBox.Text.Length == 0)
                {
                    HintDateTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    HintDateTextBlock.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие DateDatePickerTextBox_TextChanged в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void TotalAmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (TotalAmountTextBox.Text.Length == 0)
                {
                    HintTotalAmountTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    HintTotalAmountTextBlock.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие TotalAmountTextBox_TextChanged в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void CashlessPaymentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (CashlessPaymentTextBox.Text.Length == 0)
                {
                    HintCashlessPaymentTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    HintCashlessPaymentTextBlock.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие CashlessPaymentTextBox_TextChanged в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void PreviousCashBalanceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (PreviousCashBalanceTextBox.Text.Length == 0)
                {
                    HintPreviousCashBalanceTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    HintPreviousCashBalanceTextBlock.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие PreviousCashBalanceTextBox_TextChanged в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void BanckTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (BanckTextBox.Text.Length == 0)
                {
                    HintBanckTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    HintBanckTextBlock.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие BanckTextBox_TextChanged в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void HintDateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxClass.HintMessageBox_MBC(
                    textMessage: $"Параметр, который нужен для ввода даты того числа, когда создаётся подсчёт\n" +
                    $"Если приследуется цели просто посчитать данные, то без ввода даты запись не будет создана", topRow: "Информация про дату");
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие DateHintButton_Click в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void HintTotalAmountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxClass.HintMessageBox_MBC(
                    textMessage: $"Общая сумма, которая вышла за смену (на чеке напиана общая сумма, которую нужно вбить в данное поле)", topRow: "Общая сумма");
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие HintTotalAmountButton_Click в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void HintCashlessPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxClass.HintMessageBox_MBC(
                    textMessage: $"Сумма, которая вышла за смену по оплате картой (на чеке напиана сумма, которую нужно вбить в данное поле)", topRow: "Безналичный расчёт");
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие HintCashlessPaymentButton_Click в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void HintPreviousCashBalanceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxClass.HintMessageBox_MBC(
                    textMessage: $"Сумма, которая осталось в кассе с прошлой смены", topRow: "Кассовый остаток");
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие HintPreviousCashBalanceButton_Click в {mainNamePage}" + $"{ex.Message}");
            }
        }

        private void HintBanckButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxClass.HintMessageBox_MBC(
                    textMessage: $"Сумма, которая убирается в пакет (банк)\n" +
                    $"0, 500, 1000, 1500, 2000, 2500 и т.д.", topRow: "Банк");
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: $"Событие HintBanckButton_Click в {mainNamePage}" + $"{ex.Message}");
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
        private void SetToDaysDate_Click(object sender, RoutedEventArgs e)
        {
            DateDatePickerTextBox.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            double totalAmount = Convert.ToDouble(TotalAmountTextBox.Text);
            double previousCashBalance = Convert.ToDouble(PreviousCashBalanceTextBox.Text);
            double cashlessPayment = Convert.ToDouble(CashlessPaymentTextBox.Text);
            double banck = Convert.ToDouble(BanckTextBox.Text);

            CashBalanceTextBox.Text = Convert.ToString(totalAmount + previousCashBalance - cashlessPayment - banck);
            TotalForTheDayTextBox.Text = Convert.ToString(banck + cashlessPayment);

            Event_RecordingHistory();
            Event_OutputData();
        }
        #endregion
        #region Event_
        private async void Event_RecordingHistory()
        {
            string matrixRecording =
                        $"{"[Дата]",-20}{DateTime.Now.ToString("dd/MM/yyyy")}\n" +
                        $"{"[Время]",-20}{DateTime.Now.ToString("HH:mm:ss")}\n" +
                        $"{"[Общая сумма]",-20}{TotalAmountTextBox.Text}\n" +
                        $"{"[Банк]",-20}{BanckTextBox.Text}\n" +
                        $"{"[Кассовый остаток]",-20}{CashBalanceTextBox.Text}\n" +
                        $"{"[Безналичный]",-20}{CashlessPaymentTextBox.Text}\n" +
                        $"{"[Общая за день]",-20}{TotalForTheDayTextBox.Text}\n";

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                await sw.WriteLineAsync(matrixRecording);
            }
        }

        private async void Event_OutputData()
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                List<HistoryClass> historyEntries = new List<HistoryClass>();

                string line;
                HistoryClass currentEntry = new HistoryClass();

                while ((line = reader.ReadLine()) != null)
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
                        double.TryParse(line.Split('[', ']')[2].Trim(), out double сashBalanceValue);
                        currentEntry.CashBalance_HC = сashBalanceValue;
                    }
                    else if (line.Contains("[Безналичный]"))
                    {
                        double.TryParse(line.Split('[', ']')[2].Trim(), out double сashlessValue);
                        currentEntry.Cashless_HC = сashlessValue;
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
