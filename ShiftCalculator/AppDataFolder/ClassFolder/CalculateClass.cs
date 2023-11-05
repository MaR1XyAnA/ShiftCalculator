///<!--
/// Класс, в котором происходят все вычислительные действия (счёт).
/// В класс передаются значения, а потом класс выполняет определённые действия с вычислениями и возвращает их обратно.
///-->

namespace ShiftCalculator.AppDataFolder.ClassFolder
{
    internal class CalculateClass
    {
        public class WithdrawCashRegister_CC
        {
            public double CashBalance_WCR { get; set; }
            public double TotalForTheDay_WCR { get; set; }
        }

        public WithdrawCashRegister_CC _WithdrawCashRegister(double _totalAmount, double _previousCashBalance, double _cashlessPayment, double _banck)
        {
            WithdrawCashRegister_CC cashWithdrawal_CC = new WithdrawCashRegister_CC();

            cashWithdrawal_CC.CashBalance_WCR = _totalAmount + _previousCashBalance - _cashlessPayment - _banck;
            cashWithdrawal_CC.TotalForTheDay_WCR = _banck + _cashlessPayment;

            return cashWithdrawal_CC;
        }

        public class CalculateTheCostOfTheDoods_CC
        {
            public double ResultCalculations_CTCOTD { get; set; }
        }

        public CalculateTheCostOfTheDoods_CC _CalculateTheCostOfTheDoods(double _extraCharge, double _quantity, double _price)
        {
            CalculateTheCostOfTheDoods_CC calculatingMargins_CC = new CalculateTheCostOfTheDoods_CC();

            double pricePerPercentage = _extraCharge * (_price / 100);

            calculatingMargins_CC.ResultCalculations_CTCOTD = (_price + pricePerPercentage) / _quantity;

            return calculatingMargins_CC;
        }
    }
}