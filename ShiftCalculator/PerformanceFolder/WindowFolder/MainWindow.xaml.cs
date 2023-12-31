﻿///<!--
/// Главное окно, где происходят все нужные и основные действия
///-->

using ShiftCalculator.AppDataFolder.ClassFolder;
using ShiftCalculator.PerformanceFolder.PageFolder;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace ShiftCalculator.PerformanceFolder.WindowFolder
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Event_WorkWithFrame();
        }
        #region Event_
        private void Event_WorkWithFrame() /// Подключает Frame и управляет ими
        {
            FrameNavigationClass.bodySpace_FNC = BodyFrame;
            FrameNavigationClass.menuSpace_FNC = MenuFrame;
            FrameNavigationClass.menuSpace_FNC.Navigate(new MenuPage());
        }
        #endregion
        #region Управление окном
        private void TopShelGrid_MouseDown(object sender, MouseButtonEventArgs e) /// Перемищать окно
        {
            if (e.ChangedButton == MouseButton.Left) { this.DragMove(); }
        }

        private void RollupWindowButton_Click(object sender, RoutedEventArgs e) /// Свернуть окно
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e) /// Закрыть окно
        {
            Application.Current.Shutdown();
        }
        #endregion
        #region _Click
        private void OpenTheCalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("calc.exe");
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Херня, прописанная в 2 строчки нужна для того, чтобы вызвать метод, который отключит кнопки
            var currentPage = FrameNavigationClass.menuSpace_FNC.Content as MenuPage; 
            currentPage.Event_DeactivateToggleButton();

            FrameNavigationClass.bodySpace_FNC.Navigate(new SettingsPage());
        }
        #endregion
    }
}
