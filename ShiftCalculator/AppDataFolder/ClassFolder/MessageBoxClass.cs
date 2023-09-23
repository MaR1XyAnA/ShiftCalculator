///<!--
/// Класс, который нужен для настройки определённых MessageBox
/// -->


namespace ShiftCalculator.AppDataFolder.ClassFolder
{
    internal class MessageBoxClass
    {
        /// <summary>
        /// MessageBox для ошибок try{} catch (Exception ex){}
        /// </summary>
        /// <param name="textMessage"> это то, что будет выводиться (определённый текст)</param>
        /// <param name="topRow">это само название</param>
        public static void ExceptionMessageBox_MBC(
            string textMessage = "Разработчик (программист) не присвоил этому значению сообщение",
            string topRow = "Error Exception")
        {
            MessageBox.Show(
                textMessage, topRow,
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// MessageBox вывода подсказок
        /// </summary>
        /// <param name="textMessage"> это то, что будет выводиться (определённый текст)</param>
        /// <param name="topRow">это само название</param>
        public static void HintMessageBox_MBC(
            string textMessage = "Разработчик (программист) не присвоил этому значению сообщение",
            string topRow = "Разработчик (программист) не присвоил этому значению название")
        {
            MessageBox.Show(
                textMessage, topRow,
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// MessageBox вывода ошибок
        /// </summary>
        /// <param name="textMessage"> это то, что будет выводиться (определённый текст)</param>
        /// <param name="topRow">это само название</param>
        public static void ErrorMessageBox_MBC(
            string textMessage = "Разработчик (программист) не присвоил этому значению сообщение",
            string topRow = "Разработчик (программист) не присвоил этому значению название")
        {
            MessageBox.Show(
                textMessage, topRow,
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
