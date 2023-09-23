///<!--
/// Класс, который нужен для записи истории (формирует данные)
/// -->


namespace ShiftCalculator.AppDataFolder.ClassFolder
{
    /// <summary>
    /// Класс истории
    /// </summary>
    /// <param name="DateTime_HC">Дата и время</param>
    /// <param name="TotalAmount_HC">Общая</param>
    /// <param name="Bank_HC">Ушло в банк</param>
    /// <param name="CashBalance_HC">Денег в кассе</param>
    /// <param name="CashBalance_HC">Денег в кассе</param>
    /// <param name="CashBalance_HC">Денег в кассе</param>

    public class HistoryClass
    {
        public DateTime DateTime_HC { get; set; }
        public double TotalAmount_HC { get; set; }
        public double Bank_HC { get; set; }
        public double CashBalance_HC { get; set; }
        public double Cashless_HC { get; set; }
        public double TotalForTheDay_HCTotalForTheDay_HC { get; set; }
    }
}
