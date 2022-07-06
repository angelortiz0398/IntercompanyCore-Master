namespace IntercompanyCore.Documents
{
    public class DocumentLines
    {

        public string ItemCode { get; set; }
        public string Quantity { get; set; }
        public string UnitPrice { get; set; }
        public string TaxCode { get; set; }
        public string DiscountPercent { get; set; }
        public string BaseType { get; set; }
        public string BaseEntry { get; set; }
        public string BaseLine { get; set; }

        public DocumentLines(string itemCode, string quantity, string unitPrice, string discountPercent, string taxCode, string baseType, string baseEntry, string baseLine)
        {
            ItemCode = itemCode;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TaxCode = taxCode;
            DiscountPercent = discountPercent;
            BaseType = baseType;
            BaseEntry = baseEntry;
            BaseLine = baseLine;
        }
    }
}