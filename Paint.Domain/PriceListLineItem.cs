using System.ComponentModel.DataAnnotations.Schema;

namespace Paint.Domain
{
	[Table("PriceList")]
    public class PriceListLineItem
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public string Name { get; set; }
		public string MaterialSupplier { get; set; }
		public string ModelNumber { get; set; }
		public UOM UOM { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal MaterialUnitCost { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal Tax { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal MaterialTotalCost { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal LaborRate { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal LaborHours { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal LaborSubtotal { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal LaborTotal { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal ProfitMargin { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal ProfitTotal { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal TotalCost { get; set; }
	}
}
