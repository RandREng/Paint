using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Paint.Domain
{
    public class BidSheet
	{
		public int Id { get; set; }
		public int JobId { get; set; }
//		public string Address { get; set; }
//		public string ProgjectManager { get; set; }
		public DateTime Date { get; set; }
		public string SquareFoot { get; set; }
		public string BedBath { get; set; }
		public string LockBox { get; set; }
		public string Year { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal RenoTotal { get; set; }

		public List<BidArea> Areas { get; set; }

		public BidSheet()
		{
			Areas = new List<BidArea>();
		}

		[NotMapped]
		public decimal Total { get => Areas.Sum(a => a.Cost); }
	}
}
