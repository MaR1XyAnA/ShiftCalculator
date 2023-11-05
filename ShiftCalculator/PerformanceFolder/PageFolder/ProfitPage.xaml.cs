///<!--
/// Страница представляет из себя специальный калькулятор с журналом.
/// Пользователю необходимо ввести определённые данные, после чего они попадут в специальный класс, ...  
///  ... где произойдёт счёт и класс выдаст значения, которые получит пользователь. 
/// После значения записываются в журнал.
/// Журнал - это обычный .json файл, куда определённым образом просто записываются данные и всё.
///-->

using Microsoft.Win32;
using Newtonsoft.Json;
using OfficeOpenXml;
using ShiftCalculator.AppDataFolder.ClassFolder;
using ShiftCalculator.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ShiftCalculator.PerformanceFolder.PageFolder
{
    public partial class ProfitPage : Page
    {
        // Выбранные элементы
        List<HistoryClass.CashWithdrawalCalculations_HC> selectedItemList = new List<HistoryClass.CashWithdrawalCalculations_HC>();

        // Проверка полей на пустоту "нужно для метода"
        string isNullOrWhiteSpaceTextBox; 

        DispatcherTimer dispatcherTimer;

        public ProfitPage()
        {
            /// Пометка для тех, кто будет читать мой код и не поймёт прикола, ...
            /// ... а так же для новичков и для меня, если я там например уйду в армию
            
            // Так как при запуске страницы срабатывает код написанный в данном методе, то я разделил код на ...
            // ... события и запускаю события, которые запускают код (иначе код просто будет длинным)
            // InitializeComponent() - стоковое событие, я его не прописывал.

            InitializeComponent();
            Event_OutputData();
            Event_SettingsDispatcherTimer();
            Event_ToggleDateTimeControls(true);

            // А это просто сортировка по дате и времени (От самого нового к старому)
            HistoryDataGrid.Items.SortDescriptions.Add(new SortDescription("DateTime_HC", ListSortDirection.Descending));
        }

        #region _TextChanged
        private void TextHint_TextChanged(object sender, TextChangedEventArgs e) /// Показать\Спрятать текстовую подсказку
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
        private void SetToDayDateTime_Click(object sender, RoutedEventArgs e) /// В реальном времени
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

        private void CalculateButton_Click(object sender, RoutedEventArgs e) /// Работа основной кнопки
        {
            // 1. Проверяем поля на пустату
            // 2. В зависимости от результата, выполняем действие
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

        private void DeleteAnEntryButton_Click(object sender, RoutedEventArgs e) /// Удаление
        {

        }

        private void ResetSelectionButton_Click(object sender, RoutedEventArgs e) /// Сбросить выделение
        {
            HistoryDataGrid.SelectedItem = null;
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e) /// Скопировать
        {
            if (selectedItemList.Count > 0)
            {
                string copiedText = string.Join(Environment.NewLine, selectedItemList.Select(item => item.ToString()));
                Clipboard.SetText(copiedText);

                HistoryDataGrid.SelectedItem = null;
                selectedItemList = null;
            }
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = $"HistoryData {DateTime.Now.ToString("dd.MM.yyyy")}";
            saveFileDialog.Filter = "Excel Files|*.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("History Data");

                    int rowHeading = 1;
                    int rowData = 2;

                    worksheet.Cells[rowHeading, 1].Value = "Дата и время";
                    worksheet.Cells[rowHeading, 2].Value = "Сумма";
                    worksheet.Cells[rowHeading, 3].Value = "Электронными";
                    worksheet.Cells[rowHeading, 4].Value = "В кассе";
                    worksheet.Cells[rowHeading, 5].Value = "В банке";
                    worksheet.Cells[rowHeading, 6].Value = "За день";

                    //if (selectedItemList.Count > 0)
                    //{
                    //    ExcelExportHelperClass.ExportDataToWorksheet(selectedItemList, worksheet, rowData);
                    //}
                    //else
                    //{
                    //    selectedItemList = ExcelExportHelperClass.GetAllItems(HistoryDataGrid.ItemsSource);
                    //    ExcelExportHelperClass.ExportDataToWorksheet(selectedItemList, worksheet, rowData);
                    //}

                    package.SaveAs(new FileInfo(filePath));
                }
            }
        }
        #endregion
        #region Event_
        private void Event_ToggleDateTimeControls(bool enable) /// Дата и время
        {
            //В зависимости от положения кнопки, таймер вкл\выкл + спец поля вкл\выкл
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

        private void Event_DispatcherTimer(object sender, EventArgs e) /// Событие таймера
        {
            DateTextBox.Text = DateTime.Now.ToString("dd.MM.yyyy");
            TimeTextBox.Text = DateTime.Now.ToString("HH:mm:ss");
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
                isNullOrWhiteSpaceTextBox += "● Текстовое поле \"Предыдущий кассовый остаток\" не должно быть пустым.\n\n";
            }

            if (string.IsNullOrWhiteSpace(BanckTextBox.Text))
            {
                isNullOrWhiteSpaceTextBox += "● Текстовое поле \"В банке\" не должно быть пустым.";
            }
        }

        private void Event_CheckingBeforeRecording() /// Сохранение результатов
        {
            // В зависимости от состояния кнопки (вкл\выкл) и событий (дата и время) ...
            // ... выполняем \ не выполняем сохранение
            if (SaveToggleButton.IsChecked == true)
            {
                DateTime dateResult;
                TimeSpan timeResult;

                if (SetToDayDateTime.IsChecked == false)
                {
                    if (DateTime.TryParseExact(DateTextBox.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateResult))
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

        private void Event_RecordingHistory() /// Запись истории подсчёта
        {
            List<HistoryClass.CashWithdrawalCalculations_HC> recordingNewData;

            var matrixDataRecording = new HistoryClass.CashWithdrawalCalculations_HC()
            {
                DateTime_CWCC = Convert.ToDateTime(DateTextBox.Text + " " + TimeTextBox.Text),
                TotalAmount_CWCC = Convert.ToDouble(TotalAmountTextBox.Text),
                Bank_CWCC = Convert.ToDouble(BanckTextBox.Text),
                CashBalance_CWCC = Convert.ToDouble(CashBalanceTextBlock.Text),
                Cashless_CWCC = Convert.ToDouble(CashlessPaymentTextBox.Text),
                TotalForTheDay_CWCC = Convert.ToDouble(TotalForTheDayTextBlock.Text)
            };

            // Проверяем, существует ли файл
            if (File.Exists(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup))
            {
                // Если файл существует, загружаем его содержимое
                recordingNewData = JsonConvert.DeserializeObject<List<HistoryClass.CashWithdrawalCalculations_HC>>(
                    File.ReadAllText(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup));
            }
            else
            {
                // Если файл не существует или содержит некорректный JSON, создаем новый список
                recordingNewData = new List<HistoryClass.CashWithdrawalCalculations_HC>();
            }

            recordingNewData.Add(matrixDataRecording);

            // Сохраняем список в файл
            File.WriteAllText(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup,
                JsonConvert.SerializeObject(recordingNewData, Formatting.Indented));
        }

        private void Event_OutputData() // Вывод истории подсчётов
        {
            // Проверяем существование файла
            if (File.Exists(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup))
            {
                string jsonData = File.ReadAllText(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup);
                HistoryDataGrid.ItemsSource = JsonConvert.DeserializeObject<List<HistoryClass.CashWithdrawalCalculations_HC>>(jsonData);
            }
            else
            {
                List<HistoryClass.CashWithdrawalCalculations_HC> emptyHistory = new List<HistoryClass.CashWithdrawalCalculations_HC>();
                string emptyJson = JsonConvert.SerializeObject(emptyHistory);

                File.WriteAllText(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup, emptyJson);
                HistoryDataGrid.ItemsSource = emptyHistory;
            }
        }

        private void Event_PerformCountingOperation()
        {
            CalculateClass calculateClass = new CalculateClass();

            CalculateClass.WithdrawCashRegister_CC calculationResult = calculateClass._WithdrawCashRegister(
                Convert.ToDouble(TotalAmountTextBox.Text.Replace(".", ",")),
                Convert.ToDouble(PreviousCashBalanceTextBox.Text.Replace(".", ",")),
                Convert.ToDouble(CashlessPaymentTextBox.Text.Replace(".", ",")),
                Convert.ToDouble(BanckTextBox.Text.Replace(".", ",")));

            CashBalanceTextBlock.Text = calculationResult.CashBalance_WCR.ToString();
            TotalForTheDayTextBlock.Text = calculationResult.TotalForTheDay_WCR.ToString();

            CashBalanceTextBlock.Foreground = calculationResult.CashBalance_WCR < 0
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3D71"))
                : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E8BE0"));

            TotalForTheDayTextBlock.Foreground = calculationResult.TotalForTheDay_WCR < 0
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3D71"))
                : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E8BE0"));

            Event_CheckingBeforeRecording();
            Event_OutputData();
        }

        private void Event_EnteringOnlyNumbersTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex DivisionCodeRegex = new Regex("[^0-9.,]");
            e.Handled = DivisionCodeRegex.IsMatch(e.Text);
        }
        #endregion
        #region _SelectionChanged
        private void HistoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) /// Позволяет элемент или список элементов при выделении запомнить и работать
        {
            foreach (var selectedItem in HistoryDataGrid.SelectedItems)
            {
                if (selectedItem is HistoryClass.CashWithdrawalCalculations_HC historyItem)
                {
                    selectedItemList.Add(historyItem);
                }
            }

            if (HistoryDataGrid.SelectedItem != null)
            {
                DeleteAnEntryButton.IsEnabled = true;
                ResetSelectionButton.IsEnabled = true;
                CopyButton.IsEnabled = true;
            }
            else
            {
                DeleteAnEntryButton.IsEnabled = false;
                ResetSelectionButton.IsEnabled = false;
                CopyButton.IsEnabled = false;
            }
        }
        #endregion
    }
}
