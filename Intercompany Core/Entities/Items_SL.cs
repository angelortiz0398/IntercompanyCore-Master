using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntercompanyCore
{
    public class Items_SL
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public int ItemsGroupCode { get; set; }
        public char SalesItem { get; set; }
        public char InventoryItem { get; set; }
        public char PurchaseItem { get; set; }
        public char GLMethod { get; set; }
        public char CostAccountingMethod { get; set; }
        public double AvgStdPrice { get; set; }
        public char Valid { get; set; }
        public DateTime ValidTo { get; set; }
        public string BarCode { get; set; }
        public char ManageBatchNumbers { get; set; }
        public char ManageSerialNumbers { get; set; }

        public Items_SL()
        {
        }

        public Items_SL(string itemCode, string itemName, string itemType, int itemsGroupCode, char salesItem, char inventoryItem, char purchaseItem, char gLMethod, char costAccountingMethod, double avgStdPrice, char valid, DateTime validTo, string barCode, char manageBatchNumbers, char manageSerialNumbers)
        {
            ItemCode = itemCode;
            ItemName = itemName;
            ItemType = itemType;
            ItemsGroupCode = itemsGroupCode;
            SalesItem = salesItem;
            InventoryItem = inventoryItem;
            PurchaseItem = purchaseItem;
            GLMethod = gLMethod;
            CostAccountingMethod = costAccountingMethod;
            AvgStdPrice = avgStdPrice;
            Valid = valid;
            ValidTo = validTo;
            BarCode = barCode;
            ManageBatchNumbers = manageBatchNumbers;
            ManageSerialNumbers = manageSerialNumbers;
        }
    }
} 
