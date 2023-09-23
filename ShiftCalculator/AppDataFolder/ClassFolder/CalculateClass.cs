///<!--
/// Класс, в котором происходят все вычислительные действия (счёт).
/// В класс передаются значения, а потом класс выполняет определённые действия с вычислениями и возвращает их обратно.
///-->

using System;
using System.Runtime.InteropServices;

namespace ShiftCalculator.AppDataFolder.ClassFolder
{
    internal class CalculateClass
    {
        /// <summary>
        /// Подсчёт кассы
        /// </summary>
        /// <param name="CashWithdrawal">Имя доступа для получения результата</param>
        /// <param name="CashBalance_CW">Кассовый остаток</param>
        /// <param name="TotalForTheDay_CW">Общая за день</param>
        public class CashWithdrawal
        {
            public double CashBalance_CW { get; set; }
            public double TotalForTheDay_CW { get; set; }
        }

        public CashWithdrawal _CashWithdrawal(double _totalAmount, double _previousCashBalance, double _cashlessPayment, double _banck)
        {
            CashWithdrawal cashWithdrawal = new CashWithdrawal();

            cashWithdrawal.CashBalance_CW = _totalAmount + _previousCashBalance - _cashlessPayment - _banck;
            cashWithdrawal.TotalForTheDay_CW = _banck + _cashlessPayment;

            return cashWithdrawal;
        }


        /// <summary>
        /// Подсчёт наценки на товар
        /// </summary>
        /// <param name="CalculatingMargins">Имя доступа для получения результата</param>
        /// <param name="ResultCalculations_CM">Конечная стоимость</param>

        public class CalculatingMargins
        {
            public double ResultCalculations_CM { get; set; }
        }

        public CalculatingMargins _CalculatingMargins(double _extraCharge, double _quantity, double _price)
        {
            CalculatingMargins calculatingMargins = new CalculatingMargins();

            double pricePerPercentage = _extraCharge * (_price / 100);

            calculatingMargins.ResultCalculations_CM = (_price + pricePerPercentage) / _quantity;

            return calculatingMargins;
        }
    }
}