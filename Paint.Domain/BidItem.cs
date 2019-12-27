using System.ComponentModel.DataAnnotations.Schema;

namespace Paint.Domain
{
    public class BidItem
	{
		public int Id { get; set; }
		public int BidAreaId { get; set; }
		public string Sub { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal Quantity { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal UnitCost { get; set; }

		[NotMapped]
		public decimal ExtendedCost { get => Quantity * UnitCost; }
	}
}
