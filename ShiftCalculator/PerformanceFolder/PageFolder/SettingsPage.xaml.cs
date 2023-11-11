///<!--
/// Страница представляет из себя страницу настроек
///-->

using Microsoft.Win32;
using ShiftCalculator.Properties;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

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
        private void Event_AssignTheCurrentPathTextBox() /// Вывод места, где происходит сохранение
        {
            ThePathToTheFileForSavingTheHistoryOfTheMarkupTextBox.Text = Settings.Default.ThePathToTheFileForSavingTheHistoryOfTheMarkup;
            ThePathToTheFileToSaveTheShiftCountingHistoryTextBox.Text = Settings.Default.ThePathToTheFileToSaveTheShiftCountingHistory;
        }

        private void Event_AnimationStart()
        {
            NotificationBorder.Visibility = Visibility.Visible;

            TranslateTransform translateTransform = new TranslateTransform();
            DoubleAnimation animation = new DoubleAnimation();
            DoubleAnimation doubleAnimation = new DoubleAnimation();

            NotificationBorder.RenderTransform = translateTransform;
            translateTransform.X = 0;

            animation.To = -20;
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.15));
            translateTransform.BeginAnimation(TranslateTransform.XProperty, animation);

            doubleAnimation.From = 1;
            doubleAnimation.To = 0;
            doubleAnimation.Duration = TimeSpan.FromSeconds(4);
            doubleAnimation.EasingFunction = new QuadraticEase();
            NotificationBorder.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
        }
        #endregion
        #region _TextChanged
        private void HintText_TextChanged(object sender, TextChangedEventArgs e) /// Текстовые подсказки
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
        private void AssignTheCurrentPath_Click(object sender, RoutedEventArgs e) /// Открывается диалоговое окно для сохранения
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";

            Button button = (Button)sender;

            // Словарь, сопоставляющий кнопки и настройки
            Dictionary<Button, string> buttonSettingsMap = new Dictionary<Button, string>
            {
                { ThePathToTheFileForSavingTheHistoryOfTheMarkupButton, "ThePathToTheFileForSavingTheHistoryOfTheMarkup" },
                { ThePathToTheFileToSaveTheShiftCountingHistoryButton, "ThePathToTheFileToSaveTheShiftCountingHistory" }
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = saveFileDialog.FileName;

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
    }
}
