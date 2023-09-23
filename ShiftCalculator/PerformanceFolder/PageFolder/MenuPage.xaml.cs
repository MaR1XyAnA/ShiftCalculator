///<!--
/// Страница с меню. Работает просто. При нажатии на кнопку, вызывается метод, который отключает все кнопки,
///     а потом включается выбранная кнопка и выполняется переход на страницу.
///-->

using ShiftCalculator.AppDataFolder.ClassFolder;

namespace ShiftCalculator.PerformanceFolder.PageFolder
{
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            Event_StartToggleButton();
        }

        #region _Click
        private void ProfitToggleButton_Click(object sender, RoutedEventArgs e) { Event_StartToggleButton(); }

        private void ProductToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Event_DeactivateToggleButton();
            ProductToggleButton.IsChecked = true;
            FrameNavigationClass.mainSpace_FNC.Navigate(new ProductPage());
        }
        #endregion
        #region Event_
        public void Event_DeactivateToggleButton() /// Отключает всё кнопки
        {
            ProductToggleButton.IsChecked = false;
            ProfitToggleButton.IsChecked = false;
        }

        private void Event_StartToggleButton()
        {
            Event_DeactivateToggleButton();
            ProfitToggleButton.IsChecked = true;
            FrameNavigationClass.mainSpace_FNC.Navigate(new ProfitPage());
        }
        #endregion
    }
}
