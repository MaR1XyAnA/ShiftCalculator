using System.Windows;
using System.Windows.Input;

namespace ShiftCalculator.PerformanceFolder.WindowFolder
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }
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
            this.Close();
        }
        #endregion
    }
}
