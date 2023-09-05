using ShiftCalculator.AppDataFolder.ClassFolder;
using ShiftCalculator.PerformanceFolder.PageFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShiftCalculator.PerformanceFolder.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для TestWIn.xaml
    /// </summary>
    public partial class TestWIn : Window
    {
        public TestWIn()
        {
            InitializeComponent();
            HistoryDataGrid.ItemsSource = ProfitPage.selectedItemList;
        }
    }
}
