///<!--
/// Класс, который нужен для записи истории (формирует данные)
/// -->

namespace ShiftCalculator.AppDataFolder.ClassFolder
{
    internal class HistoryClass
    {
        public string Date_HC { get; set; }
        public string Time_HC { get; set; }
        public double TotalAmount_HC { get; set; }
        public double Bank_HC { get; set; }
        public double CashBalance_HC { get; set; }
        public double Cashless_HC { get; set; }
        public double TotalForTheDay_HC { get; set; }
    }
}
