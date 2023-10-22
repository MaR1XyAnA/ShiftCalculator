///<!--
/// Страница представляет из себя специальный калькулятор с журналом.
/// Пользователю необходимо ввести определённые данные, после чего они попадут в специальный класс, ...  
///  ... где произойдёт счёт и класс выдаст значения, которые получит пользователь. 
/// После значения записываются в журнал.
/// Журнал - это обычный .json файл, куда определённым образом просто записываются данные и всё.
///-->

using Newtonsoft.Json;
using ShiftCalculator.AppDataFolder.ClassFolder;
using ShiftCalculator.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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
        List<HistoryClass> selectedItemList = new List<HistoryClass>();

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
            // 2. В зависемости от результата, выполняем действие
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

        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e) /// Скачать
        {

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
            List<HistoryClass> historyClass;

            var matrixDataRecording = new HistoryClass()
            {
                DateTime_HC = Convert.ToDateTime(DateTextBox.Text + " " + TimeTextBox.Text),
                TotalAmount_HC = Convert.ToDouble(TotalAmountTextBox.Text),
                Bank_HC = Convert.ToDouble(BanckTextBox.Text),
                CashBalance_HC = Convert.ToDouble(CashBalanceTextBlock.Text),
                Cashless_HC = Convert.ToDouble(CashlessPaymentTextBox.Text),
                TotalForTheDay_HC = Convert.ToDouble(TotalForTheDayTextBlock.Text)
            };

            // Проверяем, существует ли файл
            if (File.Exists(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup))
            {
                // Если файл существует, загружаем его содержимое
                historyClass = JsonConvert.DeserializeObject<List<HistoryClass>>(
                    File.ReadAllText(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup));

                if (historyClass == null)
                {
                    // Если файл существует, но содержит некорректный JSON, создаем новый список
                    historyClass = new List<HistoryClass>();
                }
            }
            else
            {
                // Если файл не существует или содержит некорректный JSON, создаем новый список
                historyClass = new List<HistoryClass>();
            }

            historyClass.Add(matrixDataRecording);

            // Сохраняем список в файл
            File.WriteAllText(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup,
                JsonConvert.SerializeObject(historyClass, Formatting.Indented));
        }

        private void Event_OutputData() // Вывод истории подсчётов
        {
            // Проверяем существование файла
            if (File.Exists(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup))
            {
                string json = File.ReadAllText(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup);
                HistoryDataGrid.ItemsSource = JsonConvert.DeserializeObject<List<HistoryClass>>(json);
            }
            else
            {
                List<HistoryClass> emptyHistory = new List<HistoryClass>();
                string emptyJson = JsonConvert.SerializeObject(emptyHistory);

                File.WriteAllText(Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup, emptyJson);
                HistoryDataGrid.ItemsSource = emptyHistory;
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

            CashBalanceTextBlock.Foreground = calculationResult.CashBalance_CW < 0
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3D71"))
                : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E8BE0"));

            TotalForTheDayTextBlock.Foreground = calculationResult.TotalForTheDay_CW < 0
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
        #endregion
    }
}
