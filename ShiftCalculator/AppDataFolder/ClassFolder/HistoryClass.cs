///<!--
/// Класс, который нужен для записи истории (формирует данные)
/// -->

namespace ShiftCalculator.AppDataFolder.ClassFolder
{
    internal class HistoryClass
    {
        // Нужно просто для того, чтоб в DataGrid вывести номер записи (Это не ID),
        // если удалить запись под номером 13, на её место встанет другая запись (предыдущая)
        public int LineNumber_HC { get; set; } 

        public string Date_HC { get; set; }
        public string Time_HC { get; set; }
        public double TotalAmount_HC { get; set; }
        public double Bank_HC { get; set; }
        public double CashBalance_HC { get; set; }
        public double Cashless_HC { get; set; }
        public double TotalForTheDay_HC { get; set; }
    }
}
