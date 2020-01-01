using Paint.Domain;
using System;
using System.Collections.Generic;

namespace Paint.DTO
{
	public class Bid
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
		public List<BidAreaDto> Areas { get; set; }

		public Bid()
		{
			Areas = new List<BidAreaDto>();
		}

	}

}
