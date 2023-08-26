///<!--
/// Класс, в котором происходят все вычислительные действия (счёт).
/// В класс передаются значения, а потом класс выполняет определённые действия с вычислениями и возвращает их обратно.
///-->

using System;

namespace ShiftCalculator.AppDataFolder.ClassFolder
{
    internal class CalculateClass
    {
        public class BalanceResult
        {
            public double CashBalance { get; set; }
            public double TotalForTheDay { get; set; }
        }

        public BalanceResult CalculateBalances(
            double totalAmount, 
            double previousCashBalance, 
            double cashlessPayment, 
            double banck)
        {
            BalanceResult result = new BalanceResult();

            double cashBalance = totalAmount + previousCashBalance - cashlessPayment - banck;
            double totalForTheDay = banck + cashlessPayment;

            result.CashBalance = cashBalance;
            result.TotalForTheDay = totalForTheDay;

            return result;
        }
    }
}