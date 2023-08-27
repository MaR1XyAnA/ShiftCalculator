///<!--
/// Класс, в котором происходят все вычислительные действия (счёт).
/// В класс передаются значения, а потом класс выполняет определённые действия с вычислениями и возвращает их обратно.
///-->

using System;

namespace ShiftCalculator.AppDataFolder.ClassFolder
{
    internal class CalculateClass
    {
        public class BalanceResult_СС
        {
            public double CashBalance { get; set; }
            public double TotalForTheDay { get; set; }
        }

        public BalanceResult_СС CalculateBalances(
            double totalAmount, 
            double previousCashBalance, 
            double cashlessPayment, 
            double banck)
        {
            BalanceResult_СС result = new BalanceResult_СС();

            double cashBalance = totalAmount + previousCashBalance - cashlessPayment - banck;
            double totalForTheDay = banck + cashlessPayment;

            result.CashBalance = cashBalance;
            result.TotalForTheDay = totalForTheDay;

            return result;
        }

        public class ProductPrice_СС
        {
            public double ResultCalculations { get; set; }
        }
    }
}