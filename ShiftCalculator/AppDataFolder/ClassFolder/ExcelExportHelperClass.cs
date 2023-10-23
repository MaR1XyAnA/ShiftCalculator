using System.Collections.Generic;

namespace ShiftCalculator.AppDataFolder.ClassFolder
{
    public class ExcelExportHelperClass
    {
        public static List<HistoryClass.CashWithdrawalCalculationsClass> GetSelectedItems(IEnumerable<object> selectedItems)
        {
            List<HistoryClass.CashWithdrawalCalculationsClass> selectedItemList = new List<HistoryClass.CashWithdrawalCalculationsClass>();

            foreach (var item in selectedItems)
            {
                if (item is HistoryClass.CashWithdrawalCalculationsClass dataItem)
                {
                    selectedItemList.Add(dataItem);
                }
            }

            return selectedItemList;
        }

        public static List<HistoryClass.CashWithdrawalCalculationsClass> GetAllItems(IEnumerable<object> allItems)
        {
            List<HistoryClass.CashWithdrawalCalculationsClass> allItemList = new List<HistoryClass.CashWithdrawalCalculationsClass>();

            foreach (var item in allItems)
            {
                if (item is HistoryClass.CashWithdrawalCalculationsClass dataItem)
                {
                    allItemList.Add(dataItem);
                }
            }

            return allItemList;
        }
    }
}
