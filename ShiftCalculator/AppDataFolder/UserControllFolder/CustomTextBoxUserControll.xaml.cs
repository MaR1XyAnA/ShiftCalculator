using System.Windows;
using System.Windows.Controls;

namespace ShiftCalculator.AppDataFolder.UserControllFolder
{
    public partial class CustomTextBoxUserControll : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(CustomTextBoxUserControll), new PropertyMetadata(null));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(CustomTextBoxUserControll), new PropertyMetadata(null));

        public static readonly DependencyProperty HintProperty = DependencyProperty.Register(
            "Hint", typeof(string), typeof(CustomTextBoxUserControll), new PropertyMetadata(null));


        public CustomTextBoxUserControll() { InitializeComponent(); }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }
    }
}
