using System;

namespace Paint.DTO
{
    public class BidListItem
	{
		public int Id { get; set; }
		public int JobId { get; set; }
		public AddressDto Address { get; set; }
		public string ProgjectManager { get; set; }
		public DateTime Date { get; set; }
		public string SquareFoot { get; set; }
		public string BedBath { get; set; }
		public string LockBox { get; set; }
		public string Year { get; set; }
		public decimal RenoTotal { get; set; }

	}

	public class BidListItem2
	{
		public int Id { get; set; }
		public int JobId { get; set; }
		public AddressDto Address { get; set; }
		public DateTime Date { get; set; }
		public string SquareFoot { get; set; }
		public string BedBath { get; set; }
		public string LockBox { get; set; }
		public string Year { get; set; }
		public decimal RenoTotal { get; set; }

	}
}
