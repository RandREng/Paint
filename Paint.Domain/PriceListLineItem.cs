namespace Paint.Domain
{
    public class PriceListLineItem
	{
		public string Category { get; set; }
		public string SubCategory { get; set; }
		public string Name { get; set; }
		public string MaterialSupplier { get; set; }
		public string ModelNumber { get; set; }
		public UOM UOM { get; set; }
		public decimal MaterialUnitCost { get; set; }
		public decimal Tax { get; set; }
		public decimal MaterialTotalCost { get; set; }
		public decimal LaborRate { get; set; }
		public decimal LaborHours { get; set; }
		public decimal LaborSubtotal { get; set; }
		public decimal LaborTotal { get; set; }
		public decimal ProfitMargin { get; set; }
		public decimal ProfitTotal { get; set; }
		public decimal TotalCost { get; set; }
	}
}
