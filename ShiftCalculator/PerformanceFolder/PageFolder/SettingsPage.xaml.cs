using Microsoft.Win32;
using ShiftCalculator.Properties;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ShiftCalculator.PerformanceFolder.PageFolder
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            Event_AssignTheCurrentPathTextBox();
        }
        #region Event_
        private void Event_AssignTheCurrentPathTextBox()
        {
            ThePathToTheFileForSavingTheHistoryOfTheMarkupTextBox.Text = Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup;
            ThePathToTheFileToSaveTheShiftCountingHistoryTextBox.Text = Settings.Default.ThePathToTheFileToSaveTheShiftCountingHistory;
        }
        #endregion
        #region _TextChanged
        private void HintText_TextChanged(object sender, TextChangedEventArgs e) ///Текстовые подсказки
        {
            TextBox textBox = (TextBox)sender;
            TextBlock hintTextBlock = null;

            switch (textBox.Name)
            {
                case "ThePathToTheFileForSavingTheHistoryOfTheMarkupTextBox":
                    hintTextBlock = ThePathToTheFileForSavingTheHistoryOfTheMarkupTextBlock;
                    break;
                case "ThePathToTheFileToSaveTheShiftCountingHistoryTextBox":
                    hintTextBlock = ThePathToTheFileToSaveTheShiftCountingHistoryTextBlock;
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
        private void AssignTheCurrentPath_Click(object sender, RoutedEventArgs e)
        {
            // Создаем диалоговое окно для выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";

            Button button = (Button)sender;

            // Словарь, сопоставляющий кнопки и настройки
            Dictionary<Button, string> buttonSettingsMap = new Dictionary<Button, string>
            {
                { ThePathToTheFileForSavingTheHistoryOfTheMarkupButton, "ThePathToTheFileForSavingTheHistoryOfTheMarkup" },
                { ThePathToTheFileToSaveTheShiftCountingHistoryButton, "ThePathToTheFileToSaveTheShiftCountingHistory" }
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Получаем выбранный путь и сохраняем его в настройках
                string selectedFilePath = openFileDialog.FileName;

                if (buttonSettingsMap.ContainsKey(button))
                {
                    string settingKey = buttonSettingsMap[button];

                    Settings.Default[settingKey] = selectedFilePath;
                    Settings.Default.Save();

                    Event_AssignTheCurrentPathTextBox();
                }
            }
        }
        #endregion
        #region _ValueChanged
        private void RepaintBorderWithSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Color color = Color.FromArgb((byte)TransparentSlider.Value, (byte)RedSlider.Value, (byte)GreenSlider.Value, (byte)BlueSlider.Value);
            ResultBorder.Background = new SolidColorBrush(color);

            HexColorTextBox.Text = $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }
        #endregion

        private void UseACustomColorToggleButton_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.EasingFunction = new QuadraticEase();

            if (UseACustomColorToggleButton.IsChecked == true)
            {
                doubleAnimation.To = 0;
                doubleAnimation.From = 283;
            }
            else
            {
                doubleAnimation.To = 283;
                doubleAnimation.From = 0;
            }

            SettingsColorGrid.BeginAnimation(HeightProperty, doubleAnimation);
        }
    }
}
