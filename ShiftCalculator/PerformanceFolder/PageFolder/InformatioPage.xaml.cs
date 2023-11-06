using System.Diagnostics;
using System.Windows.Controls;

namespace ShiftCalculator.PerformanceFolder.PageFolder
{
    public partial class InformatioPage : Page
    {
        public InformatioPage()
        {
            InitializeComponent();
        }

        #region _Click
        private void SwitchToGitHubButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start("https://github.com/MaR1XyAnA");
        }
        #endregion
    }
}
