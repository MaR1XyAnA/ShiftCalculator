﻿using Microsoft.Win32;
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
    }
}
