using System.Collections.Generic;

namespace ShiftCalculator.AppDataFolder.ClassFolder
{
    public class ExcelExportHelperClass
    {
        public static List<HistoryClass.CashWithdrawalCalculations_HC> GetSelectedItems(IEnumerable<object> selectedItems)
        {
            List<HistoryClass.CashWithdrawalCalculations_HC> selectedItemList = new List<HistoryClass.CashWithdrawalCalculations_HC>();

            foreach (var item in selectedItems)
            {
                if (item is HistoryClass.CashWithdrawalCalculations_HC dataItem)
                {
                    selectedItemList.Add(dataItem);
                }
            }

            return selectedItemList;
        }

        public static List<HistoryClass.CashWithdrawalCalculations_HC> GetAllItems(IEnumerable<object> allItems)
        {
            List<HistoryClass.CashWithdrawalCalculations_HC> allItemList = new List<HistoryClass.CashWithdrawalCalculations_HC>();

            foreach (var item in allItems)
            {
                if (item is HistoryClass.CashWithdrawalCalculations_HC dataItem)
                {
                    allItemList.Add(dataItem);
                }
            }

            return allItemList;
        }
    }
}
