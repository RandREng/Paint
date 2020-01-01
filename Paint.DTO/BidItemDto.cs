namespace Paint.DTO
{
    public class BidItemDto
	{
		public int Id { get; set; }
		public int BidAreaId { get; set; }
		public string Sub { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }
		public decimal Quantity { get; set; }
		public decimal UnitCost { get; set; }
	}
}
