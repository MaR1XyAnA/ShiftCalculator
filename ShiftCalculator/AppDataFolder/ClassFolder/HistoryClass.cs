///<!--
/// Класс, который нужен для записи истории (формирует данные)
/// -->

using System;

namespace ShiftCalculator.AppDataFolder.ClassFolder
{
    public class HistoryClass
    {
        public class CashWithdrawalCalculations_HC /// История для подсчёта снятия кассы
        {
            public DateTime DateTime_CWCC { get; set; }
            public double TotalAmount_CWCC { get; set; }
            public double Bank_CWCC { get; set; }
            public double CashBalance_CWCC { get; set; }
            public double Cashless_CWCC { get; set; }
            public double TotalForTheDay_CWCC { get; set; }

            public override string ToString() /// Для копирования
            {
                return
                    $"Дата и время: {DateTime_CWCC}\n" +
                    $"Общая: {TotalAmount_CWCC}\n" +
                    $"Банк: {Bank_CWCC}\n" +
                    $"В кассе: {CashBalance_CWCC}\n" +
                    $"Электронными: {Cashless_CWCC}\n" +
                    $"За день: {TotalForTheDay_CWCC}\n";
            }
        }

        public class CalculationsOnTheCostOfGoods_HC /// История для подсчёта стоимости товара
        {
            public double ExtraCharge_COTCOGC { get; set; }
            public double Quantity_COTCOGC { get; set; }
            public double Costs_COTCOGC { get; set; }
            public double TotalCost_COTCOGC { get; set; }
        }
    }
}
