using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Paint.Domain
{
    public class BidArea
	{
		public int Id { get; set; }
		public int BidSheetId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<BidItem> Items { get; set; }

		public BidArea()
		{
			Items = new List<BidItem>();
		}

		[NotMapped]
		public decimal Cost { get => Items.Sum(i => i.ExtendedCost); }
	}
}
