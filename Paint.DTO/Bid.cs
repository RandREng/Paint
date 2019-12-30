using Paint.Domain;
using System;
using System.Collections.Generic;

namespace Paint.DTO
{
    public class Bid
	{
		public int Id { get; set; }
		public int JobId { get; set; }
		public string Address { get; set; }
		public string ProgjectManager { get; set; }
		public DateTime Date { get; set; }
		public string SquareFoot { get; set; }
		public string BedBath { get; set; }
		public string LockBox { get; set; }
		public string Year { get; set; }
		public decimal RenoTotal { get; set; }
		public List<BidAreaDto> Areas { get; set; }

		public Bid()
		{
			Areas = new List<BidAreaDto>();
		}

	}
	public class BidAreaDto
	{
		public int Id { get; set; }
		public int BidSheetId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<BidItemDto> Items { get; set; }

		public BidAreaDto()
		{
			Items = new List<BidItemDto>();
		}

	}

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
