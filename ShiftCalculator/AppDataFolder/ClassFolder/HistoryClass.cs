﻿
using System;
///<!--
/// Класс, который нужен для записи истории (формирует данные)
/// -->
namespace ShiftCalculator.AppDataFolder.ClassFolder
{
    public class HistoryClass
    {
        public DateTime DateTime_HC { get; set; }
        public double TotalAmount_HC { get; set; }
        public double Bank_HC { get; set; }
        public double CashBalance_HC { get; set; }
        public double Cashless_HC { get; set; }
        public double TotalForTheDay_HC { get; set; }
    }
}
