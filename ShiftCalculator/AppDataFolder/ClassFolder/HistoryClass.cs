///<!--
/// Класс, который нужен для записи истории (формирует данные)
/// -->

using System;

namespace ShiftCalculator.AppDataFolder.ClassFolder
{
    public class HistoryClass
    {
        public class CashWithdrawalCalculationsClass
        {
            public DateTime DateTime_HC { get; set; }
            public double TotalAmount_HC { get; set; }
            public double Bank_HC { get; set; }
            public double CashBalance_HC { get; set; }
            public double Cashless_HC { get; set; }
            public double TotalForTheDay_HC { get; set; }

            public override string ToString() /// Для копирования
            {
                return
                    $"Дата и время: {DateTime_HC}\n" +
                    $"Общая: {TotalAmount_HC}\n" +
                    $"Банк: {Bank_HC}\n" +
                    $"В кассе: {CashBalance_HC}\n" +
                    $"Электронными: {Cashless_HC}\n" +
                    $"За день: {TotalForTheDay_HC}\n";
            }
        }
    }
}
